using System;
using System.Collections;

namespace Lab1
{
    public class BalancedBinarySearchTree
    {
        private Node root;

        class Node
        {
            public int data;
            public int height;
            public Node left, right;

            public Node(int d)
            {
                data = d;
            }
        }

        bool isEmpty()
        {
            return root == null;
        }

        bool isFull()
        {
            return !isEmpty();
        }

        private int recursiveSize(Node current)
        {
            if (current == null) return 0;

            return 1 + recursiveSize(current.left) + recursiveSize(current.right);
        }

        int size()
        {
            return recursiveSize(root);
        }


        private Node recursiveAdd(Node current, int item)
        {
            if (current == null)
            {
                return new Node(item);
            }

            if (item < current.data)
            {
                current.left = recursiveAdd(current.left, item);
            } else if (item > current.data)
            {
                current.right = recursiveAdd(current.right, item);
            }
            else
            {
                //value already exists
                return current;
            }

            return current;
        }

        void addItem(int item)
        {
            // Normal insert
            root = recursiveAdd(root, item);
            
            // Balancing
            root = rebalance(root);
        }
        
        private int height(Node node) {
            return node == null ? -1 : node.height;
        }
        
        private void updateHeight(Node current) {
            current.height = 1 + Math.Max(height(current.left), height(current.right));
        }
        
        private int getBalance(Node node) {
            return node == null ? 0 : height(node.right) - height(node.left);
        }
        
        private Node rotateLeft(Node y) {
            Node x = y.right;
            Node z = x.left;
            x.left = y;
            y.right = z;
            updateHeight(y);
            updateHeight(x);
            return x;
        }

        private Node rotateRight(Node y) {
            Node x = y.left;
            Node z = x.left;
            x.right = y;
            y.left = z;
            updateHeight(y);
            updateHeight(x);
            return x;
        }

        private Node rebalance(Node z) {
            updateHeight(z);
            int balance = getBalance(z);
            if (balance > 1) {
                if (height(z.right.right) <= height(z.right.left)) {
                    z.right = rotateRight(z.right);
                }
                z = rotateLeft(z);
            } else if (balance < -1) {
                if (height(z.left.left) < height(z.left.right)) {
                    z.left = rotateLeft(z.left);
                }
                z = rotateRight(z);
            }
            return z;
        }
        
        private int findSmallestValue(Node node)  {
            return node.left == null ? node.data : findSmallestValue(node.left);
        }
        
        /**
     * Find node to delete, then delete it
     */
        private Node recursiveDelete(Node current, int item) {
            if (current == null) return null;

            // Node to delete found
            if (item == current.data) {
                // Node is a leaf
                if (current.left == null && current.right == null) {
                    return null;
                }

                // Node has only right child
                if (current.left == null) {
                    return current.right;
                }

                // Node has only left child
                if (current.right == null) {
                    return current.left;
                }

                // Node has both left and right children
                int smallestValue = findSmallestValue(current.right);
                current.data = smallestValue;
                current.right = recursiveDelete(current.right, smallestValue);
                return current;
            }

            if (item < current.data) {
                current.left = recursiveDelete(current.left, item);
                return current;
            }
            current.right = recursiveDelete(current.right, item);
            return current;
        }

        void deleteItem(int item) {
            root = recursiveDelete(root, item);
        }
        
        private bool recursiveSearch(Node current, int item) {
            if(current == null) {
                return false;
            }
            if (item == current.data) {
                return true;
            }
            return item < current.data
                ? recursiveSearch(current.left, item)
                : recursiveSearch(current.right, item);
        }

        bool search(int item) {
            return recursiveSearch(root, item);
        }
        
        private void printTree_preorder_recursive(Node node) {
            if(node == null) return;

            Console.Write(node.data + " ");
            printTree_preorder_recursive(node.left);
            printTree_preorder_recursive(node.right);
        }

        void printTree_preorder() {
            printTree_preorder_recursive(this.root);
            Console.Write("\n");
        }

        private void printTree_inorder_recursive(Node node) {
            if(node == null) return;

            printTree_inorder_recursive(node.left);
            Console.Write(node.data + " ");
            printTree_inorder_recursive(node.right);
        }

        void printTree_inorder() {
            printTree_inorder_recursive(this.root);
            Console.Write("\n");
        }

        private void printTree_postorder_recursive(Node node) {
            if(node == null) return;

            printTree_postorder_recursive(node.left);
            printTree_postorder_recursive(node.right);
            Console.Write(node.data + " ");
        }

        void printTree_postorder() {
            printTree_postorder_recursive(this.root);
            Console.Write("\n");
        }
    }
}