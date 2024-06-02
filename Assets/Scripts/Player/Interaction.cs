using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject { get; private set; }
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private PlayerCondition condition;
    private PlayerController controller;

    private void Awake()
    {
        condition = gameObject.GetComponent<PlayerCondition>();
        controller = gameObject.GetComponent<PlayerController>();
    }

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            var curItem = curInteractGameObject.GetComponent<ItemObject>().data;
            if (curItem.type == ItemType.Consumable)
            {
                UseItem();
                curInteractable.OnInteract();
            }
            else if(curItem.type == ItemType.Attachable)
            {
                controller.movement.moveState = MoveState.Climbing;
                controller.climb.SetRopeSegment(curInteractGameObject);
            }
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    private void UseItem()
    {
        var curItem = curInteractGameObject.GetComponent<ItemObject>().data;

        if (curItem.type == ItemType.Consumable)
        {
            var consumableItem = curItem.consumables;
            foreach (var item in consumableItem)
            {
                switch (item.type)
                {
                    case ConsumableType.Health:
                        condition.Heal(item.value);
                        break;
                    case ConsumableType.MoveSpeed:
                        StartCoroutine(controller.SpeedUp(item.value));
                        break;
                }
            }
        }
    }
}