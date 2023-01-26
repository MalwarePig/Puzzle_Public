using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class juego : MonoBehaviour
{
    public Sprite[] Niveles;

    public GameObject MenuGanar; //Declaración

    public GameObject MenuContinuar; //Ventana de mensajes

    public GameObject CamaraPrincipal;

    public GameObject PiezaSeleccionada;

    public GameObject Cronometro;

    public Text textPoema;

    int capa = 1;

    int PoemaAsignado = 0;

    public int PiezasEncajadas = 0;

    private Sprite sprite;

    string[] Poemas = new string[5];

    void Start()
    {
        StartCoroutine(GetText());
        GetText();
        CargarMensajes();
        PoemaAsignado = Random.Range(0, 5);
    }

    IEnumerator GetText()
    {
        //Debug.Log("Intentar cargar imagen desde url " + i);
        Debug.Log(PlayerPrefs.GetString("url" + PlayerPrefs.GetInt("Nivel")));
        if (PlayerPrefs.GetString("url" + PlayerPrefs.GetInt("Nivel")) != "")
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(PlayerPrefs.GetString("url" + PlayerPrefs.GetInt("Nivel"))))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    Debug.Log("cargo la textura en: " + PlayerPrefs.GetInt("Nivel"));
                    sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
                }
            }
        }
        PintarPiezza();
    }

    void PintarPiezza()
    {
        for (int i = 0; i < 36; i++)
        {
            GameObject.Find("Pieza (" + i + ")").transform.Find("Puzzle")
            .GetComponent<SpriteRenderer>()
            .sprite = sprite;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<pieza>().Encajada)
                    {
                        PiezaSeleccionada = hit.transform.gameObject;
                        PiezaSeleccionada.GetComponent<pieza>().Seleccionada =
                            true;
                        PiezaSeleccionada
                            .GetComponent<SortingGroup>()
                            .sortingOrder = capa;
                        capa++;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (PiezaSeleccionada != null)
            {
                PiezaSeleccionada.GetComponent<pieza>().Seleccionada = false;
                PiezaSeleccionada = null;
            }
        }
        if (PiezaSeleccionada != null)
        {
            Vector3 raton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PiezaSeleccionada.transform.position =
                new Vector3(raton.x, raton.y, 0);
        }

        //Comprobar si se ha completado nivel
        if (PiezasEncajadas == 36)
        {
            Cronometro.GetComponent<Tiempo>().escribirRecord(); //se comprueba el record
            if (
                PlayerPrefs.GetString("Propuesta") == "Si" //Se muestra propuesta o poemas si ya esta respondido
            )
            {
                textPoema.text = Poemas[PoemaAsignado];
                MenuContinuar.SetActive(true);
            }
            else
            {
                MenuGanar.SetActive(true);
            }
            GameObject.Find("Main Camera").GetComponent<AudioSource>().mute =
                true; //se mutea el audio de la camara (Gameplay audio)
        }
    }

    public void SiguienteNivel()
    {
        PlayerPrefs.SetString("Propuesta", "Si");
        if (
            PlayerPrefs.GetInt("Nivel") < Niveles.Length - 1 //Si o continuar
        )
        {
            PlayerPrefs.SetInt("Nivel", PlayerPrefs.GetInt("Nivel") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Nivel", 0);
        }
        SceneManager.LoadScene("Juego");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    void CargarMensajes(){
        for (int i = 0; i < 5; i++)
        {
            Poemas[i] = PlayerPrefs.GetString("Poema" + i);
        }

        for (int i = 0; i < 5; i++)
        {
           Debug.Log(Poemas[i]);
        }
    }




}
