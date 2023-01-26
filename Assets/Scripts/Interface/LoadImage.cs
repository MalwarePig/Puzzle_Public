using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadImage : MonoBehaviour
{

    public Button openExplorerButton;
    public Button saveUrlButton;
    public GameObject menuURL; // Assign in inspector
    private bool isShowing = true;
    [SerializeField] private Image[] Puzzle;

    private string[] url = new string[10];
    private Sprite[] newSprite = new Sprite[10];

    void Start()
    {
        CargarUrlsMenu();//Cargar urls a menu si existen desde PlayerPrefs
                         // Debug.Log("Star");
        StartCoroutine(DescargarImagen());
    }

    IEnumerator DescargarImagen()
    {
        for (int i = 0; i < 10; i++)
        {
            //Debug.Log("Intentar cargar imagen desde url " + i);
            Debug.Log(PlayerPrefs.GetString("url"+i));
            if (PlayerPrefs.GetString("url"+i) != "")
            {
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(GameObject.Find("url" + i).GetComponent<InputField>().text))
                {
                    yield return uwr.SendWebRequest();

                    if (uwr.isNetworkError || uwr.isHttpError)
                    {
                        Debug.Log(uwr.error);
                    }
                    else
                    {
                   
                        // Get downloaded asset bundle
                        var texture = DownloadHandlerTexture.GetContent(uwr);
                        Debug.Log("cargo la textura en: " + i);
                        newSprite[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
                        Puzzle[i].sprite = newSprite[i];
                        //
                    }
                }
            }  
        } 
        MostrarURls();
    }


    void MostrarURls()//Muestra la lista de urls
    {
        isShowing = !isShowing;
        menuURL.SetActive(isShowing);
    }

    void GuardarUrls()//Guardar urls de inputs a PlayerPrefs
    {
        for (int i = 0; i < 10; i++)
        {
            url[i] = GameObject.Find("txtUrl" + i.ToString()).GetComponent<Text>().text;
            PlayerPrefs.SetString("url" + i, url[i]);
        }
        Application.LoadLevel(Application.loadedLevel);
    }

    void CargarUrlsMenu()//Cargar urls a menu si existen desde PlayerPrefs
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.GetString("url" + i) != "")
            {
                //Debug.Log("urls cargadas anteriormente: " + PlayerPrefs.GetString("url" + i));
                GameObject.Find("url" + i).GetComponent<InputField>().text = PlayerPrefs.GetString("url" + i);
                GameObject.Find("url" + i).GetComponent<Image>().color = Color.green;
            }
        }
    }

    void OnEnable()
    {
        openExplorerButton.onClick.AddListener(delegate { MostrarURls(); });
        saveUrlButton.onClick.AddListener(delegate { GuardarUrls(); });
    }








}
