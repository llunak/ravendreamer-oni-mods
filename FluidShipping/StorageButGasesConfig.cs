﻿// Decompiled with JetBrains decompiler
// Type: StorageLockerConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C4FA4E6F-758D-4D97-B8F6-20B31F856ED3
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\OxygenNotIncluded\OxygenNotIncluded_Data\Managed\Assembly-CSharp.dll

using STRINGS;
using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

public class StorageButGasesConfig : IBuildingConfig
{
	//public const string ID = "Gas.StorageLocker";

	public override BuildingDef CreateBuildingDef()
	{
		string id = ID;
		int width = 1;
		int height = 2;
		string anim = "stormshark_canisterinserter_kanim";
		int hitpoints = 30;
		float construction_time = 10f;
		float[] tieR4 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] rawMinerals = MATERIALS.RAW_MINERALS;
		float melting_point = 1600f;
		BuildLocationRule build_location_rule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tieR4, rawMinerals, melting_point, build_location_rule, TUNING.BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		return buildingDef;
	}

	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		//SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.showInUI = true;
		storage.allowItemRemoval = false;
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.GASES;
		storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
		storage.capacityKg = 50;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Gas;
		conduitDispenser.alwaysDispense = true;
		go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
		go.AddOrGet<StorageLocker>();
	}

	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<StorageController.Def>();
	}

	public const string ID = "StormShark.CanisterInserter";
	static readonly string Name = "Canister Inserter";
	static readonly string Description = "Canister Inserters allow contained gas to be inserted directly into a ventilation network.";
	static readonly string Effect = "Loads " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " canisters into " + UI.FormatAsLink("Pipes", "GASPIPING") + " for transport.\n\nMust be loaded by Duplicants.";
	public static void Setup()
	{
		Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ID.ToUpperInvariant()}.NAME", "<link=\"" + ID + "\">" + Name + "</link>");
		Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ID.ToUpperInvariant()}.DESC", Description);
		Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ID.ToUpperInvariant()}.EFFECT", Effect);


		int categoryIndex = TUNING.BUILDINGS.PLANORDER.FindIndex(x => x.category == "HVAC");
		(TUNING.BUILDINGS.PLANORDER[categoryIndex].data as IList<String>)?.Add(ID);

		var TechGroup = new List<string>(Database.Techs.TECH_GROUPING["Distillation"]) { };
		TechGroup.Add(ID);
		Database.Techs.TECH_GROUPING["Distillation"] = TechGroup.ToArray();

	}
}
