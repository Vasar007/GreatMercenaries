﻿using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    // Class which contains logic for blocking actions.
    public class BlockInstance
    {
        public CardInstance cardAttacker;
        public List<CardInstance> cardBlockers = new List<CardInstance>();
    }
}
