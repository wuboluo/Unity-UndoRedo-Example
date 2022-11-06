using UnityEngine;

public class InputPlane : MonoBehaviour
{
    public Transform objPrefab;
    private RaycastHit hitInfo;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                // 创建一条关于生成物体的命令，并由命令调用器追踪
                ICommand command = new PlaceObjCommand(objPrefab, hitInfo.point, Random.Range(0, 9));
                GetComponent<CommandInvoker>().AddCommand(command);
            }
        }
    }
}