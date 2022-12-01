/*  Filename:           TyrannosaurusState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        abstract Tyrannosaurus State
 *  Revision History:   November 26 (Yuk Yee Wong): Inital Script.
 */

using UnityEngine;

public abstract class TyrannosaurusState : BaseState<TyrannosaurusController>
{
    protected TyrannosaurusState(TyrannosaurusController context) : base(context)
    {
    }
}
