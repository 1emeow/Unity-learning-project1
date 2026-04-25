using UnityEngine;

public class Catapult_StateController : StateMachine
{
    [Header("Param�tres de changement d'�tat/d'�tats")]
    // Ici, on mettera les r�f�rences aux diff�rents �tats que le catapult peut avoir, par exemple : saut, chute etc.


    [Header("R�f�rences aux diff�rents animators et audioSources")]
    // Ici, on mettra les r�f�rences aux diff�rents animators et audioSources que le catapult peut utiliser pour ses diff�rentes animations et sons.


    [Header("Les �tats utilis�s par la Catapulte")]
    // Ici, on mettra les r�f�rences aux diff�rents �tats que le catapult peut avoir, par exemple : saut, chute etc.
    public Catapult_IdleState idleState;

    //Dans Awake, on initialise les diff�rents �tats et on d�finit l'�tat de d�part de la catapulte.
    private void Awake()
    {

    }
}
