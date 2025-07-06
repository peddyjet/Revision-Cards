﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabMaster : MonoBehaviour
{
    [Header("Colours")]
    public Color closed;
    public Color open;
    public Color hovering;

    public List<Tab> Tabs { get; private set; } = new List<Tab>();

    public void Subscribe(Tab tab) => Tabs.Add(tab);

    public void OpenTab(Tab tab)
    {
        foreach (var i in Tabs)
        {
            i.CloseTab();
        }

        Tabs[Tabs.IndexOf(tab)].OpenTab();
    }

    private void Start()
    {
        foreach (var i in Tabs)
        {
            i.CloseTab();
        }
    }

}