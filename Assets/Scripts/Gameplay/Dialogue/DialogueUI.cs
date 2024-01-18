using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    #region Variables
    [SerializeField]
    public ConversantController conversantController;
    [SerializeField]
    TextMeshProUGUI speakerText;
    [SerializeField]
    Button nextButton;
    [SerializeField]
    Transform dialogueVariant;
    [SerializeField]
    GameObject dialogueVariantPrefab;
    [SerializeField]
    GameObject dialogueResponse;
    [SerializeField]
    Button quitButton;
    [SerializeField] 
    TextMeshProUGUI conversantName;
    #endregion

    void Start()
    {
        //conversantController = GameObject.FindGameObjectWithTag("Player").GetComponent<ConversantController>();
        conversantController.OnConversantUpdate += UpdateUI;

        //quitButton.onClick.AddListener(() => conversantController.QuitDialogue());
        //nextButton.onClick.AddListener(() => conversantController.SelectNextDialogueVariant());

        UpdateUI();
    }

    
    void UpdateUI()
    {
        gameObject.SetActive(conversantController.IsActive());

        if (!conversantController.IsActive())
        {
            return;
        }

        conversantName.text = conversantController.GetCurrentConversantName();

        dialogueResponse.SetActive(!conversantController.IsChoosing());
        dialogueVariant.gameObject.SetActive(conversantController.IsChoosing());
        
        if (conversantController.IsChoosing())
        {
            CreateVariantList();
        }

        else 
        {
            speakerText.text = conversantController.GetText();
            //nextButton.gameObject.SetActive(conversantController.HasNext());
        }
    }

    private void CreateVariantList()
    {
        foreach (Transform variantChild in dialogueVariant)
        {
            Destroy(variantChild.gameObject);
        }

        foreach (DialogueNode choiceText in conversantController.GetChoiceVariants())
        {
            GameObject choice = Instantiate(dialogueVariantPrefab, dialogueVariant);
            TextMeshProUGUI textComponent = choice.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = choiceText.GetText();
            Button btn = choice.GetComponentInChildren<Button>();

            btn.onClick.AddListener(() => 
            { 
                conversantController.SelectChoice(choiceText);
            });
        }
    }
}
