using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    // 命令缓存队列，记录被添加的命令，需要按照添加顺序依次执行命令，即先进先出
    private static Queue<ICommand> commandBuffer;
    private static List<ICommand> commandHistory;

    // 当前阶段处于哪条命令状态
    [SerializeField] private int counter;

    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
    }

    private void Update()
    {
        // 命令缓存池中为排好队等待执行的命令，当存在时，依次执行，并添加历史命令记录和命令数量
        if (commandBuffer.Count > 0)
        {
            // 执行一条新的命令
            var c = commandBuffer.Dequeue();
            c?.Redo();

            commandHistory.Add(c);
            counter++;
            Debug.Log($"当前命令序号：{counter}");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log($"<color=red>按下Z：当前命令序号：{counter}</color>");
                // 若当前还有存在的命令（理解为不为初始状态），则可以撤销命令，即撤销
                if (counter > 0)
                {
                    counter--;
                    commandHistory[counter].Undo();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log($"<color=green>按下Y：当前命令序号：{counter}, 历史总命令数：{commandHistory.Count}</color>");
                // 若当前命令不为最后一条命令（理解为不是最新状态），则可以前进至下一条命令，即执行
                if (counter < commandHistory.Count)
                {
                    commandHistory[counter].Redo();
                    counter++;
                }
            }
        }
    }

    public void AddCommand(ICommand command)
    {
        // 如果在多条命令之间插入新的命令（在可撤回且可重做之间，创建了新的 Obj）
        // 则以此为分界线，将重做的命令移除（理解为时间宝石修改历史，创建新的时间线）
        while (commandHistory.Count > counter) commandHistory.RemoveAt(counter);

        commandBuffer.Enqueue(command);
    }
}