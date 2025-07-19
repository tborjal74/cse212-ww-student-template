using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty"
    // Defect(s) Found: None
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue 3 items with different priorities
    // Expected Result: Dequeue returns highest priority item ("B")
    // Defect(s) Found: None
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5); // Highest priority
        priorityQueue.Enqueue("C", 3);

        string result = priorityQueue.Dequeue();
        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Enqueue items with the same highest priority
    // Expected Result: Dequeue returns the first inserted among those with highest priority (FIFO)
    // Defect(s) Found: None
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("X", 2);
        priorityQueue.Enqueue("Y", 5); // First with highest priority
        priorityQueue.Enqueue("Z", 5); // Second with same highest

        string result1 = priorityQueue.Dequeue(); // Should be "Y"
        string result2 = priorityQueue.Dequeue(); // Then "Z"

        Assert.AreEqual("Y", result1);
        Assert.AreEqual("Z", result2);
    }

    [TestMethod]
    // Scenario: Multiple enqueues and dequeues
    // Expected Result: Items dequeued in correct priority order
    // Defect(s) Found: None
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Task1", 2);
        priorityQueue.Enqueue("Task2", 1);
        priorityQueue.Enqueue("Task3", 3);
        priorityQueue.Enqueue("Task4", 3); // Same as Task3

        Assert.AreEqual("Task3", priorityQueue.Dequeue()); // First with highest priority
        Assert.AreEqual("Task4", priorityQueue.Dequeue()); // Second with same
        Assert.AreEqual("Task1", priorityQueue.Dequeue());
        Assert.AreEqual("Task2", priorityQueue.Dequeue());
    }
}