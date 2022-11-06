using UnityEngine;

public class PlaceObjCommand : ICommand
{
    private readonly int id;
    private readonly Transform obj;
    private readonly Vector3 position;

    public PlaceObjCommand(Transform obj, Vector3 position, int id)
    {
        this.obj = obj;
        this.position = position;
        this.id = id;
    }

    // 重做，前进命令
    public void Redo()
    {
        ObjPalcer.PlaceObj(obj, position, id);
    }

    // 撤销，回退命令
    public void Undo()
    {
        ObjPalcer.RemoveObj(position, id);
    }
}