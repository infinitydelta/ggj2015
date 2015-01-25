#if UNITY_EDITOR
//////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2014 Audiokinetic Inc. / All Rights Reserved
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;



public class AkWwiseXMLWatcher
{
	private FileSystemWatcher XmlWatcher;
	private string WwiseProjectFolder;
	
	private static AkWwiseXMLWatcher Instance = null;
	
	public static AkWwiseXMLWatcher GetInstance()
	{
		if (Instance == null)
		{
			Instance = new AkWwiseXMLWatcher ();
		}
		
		return Instance;
	}
	
	
	private AkWwiseXMLWatcher()
	{
		XmlWatcher 			= new FileSystemWatcher ();
		WwiseProjectFolder 	= Path.GetDirectoryName(AkUtilities.GetFullPath(Application.dataPath, WwiseSettings.LoadSettings().WwiseProjectPath));
		
		try
		{
			XmlWatcher.Path = WwiseProjectFolder;
			XmlWatcher.NotifyFilter = NotifyFilters.LastWrite; 
			
			// Event handlers that are watching for specific event
			XmlWatcher.Created += new FileSystemEventHandler(RaisePopulateFlag);
			XmlWatcher.Changed += new FileSystemEventHandler(RaisePopulateFlag);
			
			XmlWatcher.Filter = "*.xml";
			XmlWatcher.IncludeSubdirectories = true;
		}
		catch( Exception e )
		{
			Debug.LogError(e.ToString());
		}
	}
	
	public void StartXMLWatcher()
	{
		XmlWatcher.EnableRaisingEvents = true; 
	}
	
	public void StopXMLWatcher()
	{
		XmlWatcher.EnableRaisingEvents = false;
	}

	
	void RaisePopulateFlag(object sender, FileSystemEventArgs e)
	{	
		// Signal the main thread it's time to populate (cannot run populate somewhere else than on main thread)
		AkAmbientInspector.populateSoundBank = true;
	}
}
#endif