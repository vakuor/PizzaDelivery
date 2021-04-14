using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum Pizza{
        Pepperoni,
        Margarita
    }
    [SerializeField] private Pizza pizzaSelector;
    
    [SerializeField] private AmmoData PepperoniPizza;
    [SerializeField] private AmmoData MargaritaPizza;
    private AmmoData weapon;
    private PlayerMovement playerMovement;
    private PlayerAction playerAction;
    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        playerAction = GetComponent<PlayerAction>();
        switch(pizzaSelector){
            case Pizza.Margarita: {
                weapon = MargaritaPizza;
                break;
            }
            case Pizza.Pepperoni: {
                weapon = PepperoniPizza;
                break;
            }
        }
        playerMovement.CurrentAmmoData = weapon;
        playerAction.CurrentAmmoData = weapon;
    }
}
