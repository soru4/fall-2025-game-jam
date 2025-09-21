// This file must be placed in a folder named 'Editor' in your Unity project.

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Linq;
using System.IO;

public class DisplayQuestGraph : EditorWindow
{
	private TreeView questTreeView;
	private TreeViewState treeViewState;
	private QuestNodeData selectedNodeData;
	private Vector2 scrollPosition;

	// Use a serializable class to store node data for easier serialization
	[System.Serializable]
	public class QuestNodeData
	{
		public QuestTNode node;
		public string title;
	}

	[MenuItem("Tools/Display Quest Graph")]
	public static void ShowWindow()
	{
		GetWindow<DisplayQuestGraph>("Display Quest Graph");
	}

	private void OnEnable()
	{
		if (treeViewState == null)
			treeViewState = new TreeViewState();

		questTreeView = new QuestNodeTreeView(treeViewState);
		questTreeView.Reload();
		
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

		// Tree View pane
		EditorGUILayout.BeginVertical("box", GUILayout.Width(position.width * 0.4f));
		EditorGUILayout.LabelField("Quest Nodes", EditorStyles.boldLabel);

		questTreeView.OnGUI(EditorGUILayout.GetControlRect(GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)));

		if (questTreeView.GetSelection().Count > 0)
		{
			int selectedId = questTreeView.GetSelection()[0];
            
			// Use GetRows() and LINQ to find the base item.
			var baseItem = questTreeView.GetRows().FirstOrDefault(x => x.id == selectedId);

			if (baseItem != null)
			{
				// Correctly cast the base item to our custom class
				var selectedNodeItem = baseItem as QuestNodeTreeViewItem;
				if (selectedNodeItem != null && selectedNodeItem.node != null)
				{
					selectedNodeData = new QuestNodeData { node = selectedNodeItem.node, title = selectedNodeItem.displayName };
				}
			}
		}
		EditorGUILayout.EndVertical();

		// Details pane
		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("Node Details", EditorStyles.boldLabel);

		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
		if (selectedNodeData != null && selectedNodeData.node != null)
		{
			// Draw a custom inspector for the selected QuestTNode
			EditorGUILayout.LabelField("Title:", selectedNodeData.title);
			DrawQuestNodeDetails(selectedNodeData.node);
		}
		else
		{
			EditorGUILayout.LabelField("Select a Quest Node from the tree to view its details.");
		}
		EditorGUILayout.EndScrollView();

		// Add/Remove buttons
		GUILayout.FlexibleSpace();
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Create Child Node"))
		{
			ShowAddNodeContextMenu();
		}
		if (GUILayout.Button("Create Loose Node"))
		{
			CreateFreestandingNode();
		}
		EditorGUI.BeginDisabledGroup(selectedNodeData == null);
		if (GUILayout.Button("Remove Node"))
		{
			RemoveSelectedNode();
		}
		EditorGUI.EndDisabledGroup();
		if (GUILayout.Button("Refresh"))
		{
			questTreeView.Reload();
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
	}

	private void DrawQuestNodeDetails(QuestTNode node)
	{
		if (node == null) return;

		// Use a SerializedObject to correctly handle modifications to the ScriptableObject asset
		SerializedObject serializedObject = new SerializedObject(node);
		SerializedProperty iterator = serializedObject.GetIterator();

		// Draw all serialized properties of the QuestTNode
		if (iterator.NextVisible(true))
		{
			do
			{
				EditorGUILayout.PropertyField(iterator, true);
			}
				while (iterator.NextVisible(false));
		}

		serializedObject.ApplyModifiedProperties();
	}

	private void ShowAddNodeContextMenu()
	{
		if (selectedNodeData == null || selectedNodeData.node == null)
		{
			Debug.LogWarning("Please select a node to add a child to.");
			return;
		}

		GenericMenu menu = new GenericMenu();

		// Add options based on available links and player responses
		if (!string.IsNullOrEmpty(selectedNodeData.node.playerResponse1))
		{
			if (selectedNodeData.node.response1Next == null)
			{
				menu.AddItem(new GUIContent("Add to response1Next"), false, CreateAndLinkNode, "response1Next");
			}
			else
			{
				menu.AddDisabledItem(new GUIContent("Add to response1Next (Already occupied)"));
			}
		}
        
		if (!string.IsNullOrEmpty(selectedNodeData.node.playerResponse2))
		{
			if (selectedNodeData.node.response2Next == null)
			{
				menu.AddItem(new GUIContent("Add to response2Next"), false, CreateAndLinkNode, "response2Next");
			}
			else
			{
				menu.AddDisabledItem(new GUIContent("Add to response2Next (Already occupied)"));
			}
		}

		if (selectedNodeData.node.next == null)
		{
			menu.AddItem(new GUIContent("Add to next"), false, CreateAndLinkNode, "next");
		}
		else
		{
			menu.AddDisabledItem(new GUIContent("Add to next (Already occupied)"));
		}

		menu.ShowAsContext();
	}

	private void CreateAndLinkNode(object linkType)
	{
		if (selectedNodeData == null || selectedNodeData.node == null)
		{
			return;
		}

		// Create the new asset instance.
		string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedNodeData.node));
		QuestTNode newNode = ScriptableObject.CreateInstance<QuestTNode>();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New Quest Node.asset");
		AssetDatabase.CreateAsset(newNode, assetPathAndName);

		// Link the new node based on the selected option
		QuestTNode parentNode = selectedNodeData.node;
		SerializedObject serializedParent = new SerializedObject(parentNode);
		string linkPropertyName = (string)linkType;

		serializedParent.FindProperty(linkPropertyName).objectReferenceValue = newNode;
		serializedParent.ApplyModifiedProperties();

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		questTreeView.Reload();
	}

	private void CreateFreestandingNode()
	{
		// Get the path to create the new asset.
		string path = "Assets/";
        
		// Create the new asset instance.
		QuestTNode newNode = ScriptableObject.CreateInstance<QuestTNode>();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/Freestanding Quest Node.asset");
		AssetDatabase.CreateAsset(newNode, assetPathAndName);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
        
		// The new node is not linked to any other node.
        
		questTreeView.Reload();
	}

	private void RemoveSelectedNode()
	{
		if (selectedNodeData == null || selectedNodeData.node == null)
		{
			return;
		}

		// Find and remove links to the selected node
		string[] guids = AssetDatabase.FindAssets("t:QuestTNode");
		foreach (string guid in guids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			QuestTNode node = AssetDatabase.LoadAssetAtPath<QuestTNode>(path);
			if (node != null)
			{
				SerializedObject serializedNode = new SerializedObject(node);
				bool updated = false;

				if (node.response1Next == selectedNodeData.node)
				{
					serializedNode.FindProperty("response1Next").objectReferenceValue = null;
					updated = true;
				}
				if (node.response2Next == selectedNodeData.node)
				{
					serializedNode.FindProperty("response2Next").objectReferenceValue = null;
					updated = true;
				}
				if (node.next == selectedNodeData.node)
				{
					serializedNode.FindProperty("next").objectReferenceValue = null;
					updated = true;
				}

				if (updated)
				{
					serializedNode.ApplyModifiedProperties();
				}
			}
		}

		// Delete the selected asset
		AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(selectedNodeData.node));
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
        
		selectedNodeData = null;
		questTreeView.Reload();
	}

	// A custom TreeView implementation for QuestTNodes
	private class QuestNodeTreeView : TreeView
	{
		private int nextId = 1;

		public QuestNodeTreeView(TreeViewState treeViewState) : base(treeViewState)
		{
			showBorder = true;
			Reload();
		}
        
		
			
		

		protected override TreeViewItem BuildRoot()
		{
			var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
			var allNodes = AssetDatabase.FindAssets("t:QuestTNode")
				.Select(guid => AssetDatabase.LoadAssetAtPath<QuestTNode>(AssetDatabase.GUIDToAssetPath(guid)))
				.Where(node => node != null)
				.ToList();

			// Find all nodes that are not referenced by any other node (the true root nodes)
			var childNodes = new HashSet<QuestTNode>();
			foreach (var node in allNodes)
			{
				if (node.response1Next != null) childNodes.Add(node.response1Next);
				if (node.response2Next != null) childNodes.Add(node.response2Next);
				if (node.next != null) childNodes.Add(node.next);
			}

			var rootNodes = allNodes.Where(node => !childNodes.Contains(node)).ToList();

			List<TreeViewItem> items = new List<TreeViewItem>();
			var visitedNodes = new HashSet<QuestTNode>();

			foreach (var rootNode in rootNodes)
			{
				// Recursively add children for each root node
				AddChildrenRecursively(rootNode, items, 0, visitedNodes);
			}
            
			SetupParentsAndChildrenFromDepths(root, items);
			return root;
		}

		private void AddChildrenRecursively(QuestTNode node, List<TreeViewItem> items, int depth, HashSet<QuestTNode> visitedNodes)
		{
			if (node == null || visitedNodes.Contains(node))
			{
				return;
			}

			visitedNodes.Add(node);

			var item = new QuestNodeTreeViewItem { id = nextId++, depth = depth, displayName = node.name, node = node };
			items.Add(item);

			// Recursively add children based on the specific node links
			AddChildrenRecursively(node.response1Next, items, depth + 1, visitedNodes);
			AddChildrenRecursively(node.response2Next, items, depth + 1, visitedNodes);
			AddChildrenRecursively(node.next, items, depth + 1, visitedNodes);
		}
	}

	// A custom TreeViewItem to hold our QuestTNode data
	private class QuestNodeTreeViewItem : TreeViewItem
	{
		public QuestTNode node;
	}
}