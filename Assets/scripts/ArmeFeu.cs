using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeFeu : MonoBehaviour
{
    //Statistisques
    public int degats;
    public float CadenceTir, Dispersion, Portee, TempsRechargement, TempsEntreChaqueTir;
    public int TailleChargeur, BalleParClics;
    public bool MaintenirTir;
    private int BallesRestantes, BallesUtilise;


    //bools (valeurs qui peuvent seulement etre vrai ou faux)
    bool Tir, PretATirer, Rechargement;

    //References
    public Camera fpsCam;
    public Transform PointAttaque;
    public RaycastHit rayHit;
    public LayerMask EnnemieOuPas;


    //Tremblement caméra ?? A faire dans le script FPS Controller ou ici, On verra plus tard
    private void Start()
    {
        BallesRestantes = TailleChargeur;
        PretATirer = true;
    }
    private void Update()
    {
        MesControlles();
    }
    private void MesControlles()
    {
        if (MaintenirTir) Tir = Input.GetKey(KeyCode.Mouse0);
        else Tir = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && BallesRestantes < TailleChargeur && !Rechargement) Recharge();


        //Tir
        if (PretATirer && Tir && !Rechargement && BallesRestantes > 0)
        {
            BallesUtilise = BalleParClics;
            EffectuerTir();
        }
        
    }

    private void EffectuerTir()
    {
        PretATirer = false;

        //Dispersion
        float x = Random.Range(-Dispersion, Dispersion);
        float y = Random.Range(-Dispersion, Dispersion);


        //calcul de la direction avec la dispersion
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);




        //Dispersion pendant que le joueur se déplace 
        //Si j'utilise un RigidBody : if (rigidbody.velocity.magnitude > 0)
                                     //else Dispersion ="viesse normal;
                                    
        //Je peut aussi dire que quand le joueur appui sur une touche de déplcament ZQSD ou WASD la dispersion doit augmenter A FAIRE PLUS TARD




        //RayCast (Detection des objets dans l'environnement 3d)
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, Portee, EnnemieOuPas))
        {
            Debug.Log(rayHit.collider); //Juste pour verifier que ça marche, .name après collider?

            if (rayHit.collider.CompareTag("Enemmi")) ;
                //rayHit.collider.GetComponent<TirSurIA>().ReçoisDegats(degats);   //NE MARCHE PAS     //L'ennemi a doit etre avoir un tag "Ennemi" et doit avoir un script avec la fonction ReçoisDegats
        }

        BallesRestantes--;
        BallesUtilise--;


        Invoke("ResetTir", TempsEntreChaqueTir);

        if(BallesUtilise > 0 && BallesRestantes > 0)
        Invoke("Tir", TempsEntreChaqueTir);
    }
    private void ResetTir()
    {
        PretATirer = true;
    }





    private void Recharge()
    {
        Rechargement = true;
        Invoke("FinRechargement", TempsRechargement);
    }


    private void FinRechargement()
    {
        BallesRestantes = TailleChargeur;
        Rechargement = false;
    }
}
