/*  Filename:           ApatosaurusState.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        Abstract Apatosaurus state
 *  Revision History:   December 12, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public abstract class ApatosaurusState : BaseState<ApatosaurusController>
{
    protected ApatosaurusState(ApatosaurusController context) : base(context)
    {
    }
}
