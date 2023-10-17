Console.WriteLine("Started!");

var numThreads = 2;
var threadPool = new Dictionary<string, Thread>();

var list = Enumerable.Range(0, 100).ToArray();
var chunkSize = list.Length / numThreads;

for (int i = 0; i < numThreads; i++)
{
    var chunk = list.Skip(i * chunkSize).Take(chunkSize).ToArray();
    var id = Guid.NewGuid().ToString();

    var thread = new Thread(() => doThings(id, chunk)) { IsBackground = true };
    threadPool.Add(id, thread);
    thread.Start();
}

while (threadPool.Count > 0)
    continue;

Console.WriteLine("Ended!");

void doThings(string threadId, int[] chunk)
{
    for (int i = 0; i < chunk.Length; i++)
    {
        Console.WriteLine($"Thread {threadId}: Operation {chunk[i]}");
        Thread.Sleep(1000);
    }

    threadPool.Remove(threadId);
}
