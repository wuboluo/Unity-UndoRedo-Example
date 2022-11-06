public interface ICommand
{
    // 执行命令
    void Redo();

    // 撤销命令
    void Undo();
}