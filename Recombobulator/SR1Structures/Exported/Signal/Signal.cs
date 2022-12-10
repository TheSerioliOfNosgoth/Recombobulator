using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Signal : SR1_Structure
	{
		public enum SignalTypeJun01
		{
			HandleLightGroup,           // long lightGroup;
			HandleCameraAdjust,         // long cameraAdjust;
			HandleCameraMode,           // long cameraMode;
			HandleCameraKey,            // CameraKey *cameraKey
			HandleCameraTimer,          // long cameraTimer;
			HandleCameraSmooth,         // long cameraSmooth;
			HandleCameraValue,          // long index; long value;
			HandleCameraLock,           // long cameraLock;
			HandleCameraUnlock,         // long cameraUnlock;
			HandleCameraSave,           // long cameraSave;
			HandleCameraRestore,        // long cameraRestore;
			HandleFogNear,              // long fogNear;
			HandleFogFar,               // long fogFar;
			HandleCameraShake,          // long time; long scale;
			HandleCallSignal,           // void* callSignal; (callSignal is itself a signal?)
			HandleEnd,                  // (nothing)
			HandleStopPlayerControl,    // (nothing)
			HandleStartPlayerControl,   // (nothing)
			HandleStreamLevel,          // long currentnum; long streamID; char toname[16];
			HandleCameraSpline,         // long index; Intro* intro;
			HandleScreenWipe,           // short type; short time;
			HandleBlendStart,           // long blendStart;
			HandleScreenWipeColor,      // unsigned char r; unsigned char g; unsigned char b; unsigned char pad;
			HandleSetSlideAngle,        // long slideAngle;
			HandleResetSlideAngle,      // (nothing)
			HandleSetCameraTilt,        // long cameraTilt;
			HandleSetCameraDistance,    // long cameraDistance;
		}
		public enum SignalTypeMay12
		{
			HandleLightGroup = 12,      // long lightGroup;
			HandleCameraAdjust,         // long cameraAdjust;
			HandleCameraMode,           // long cameraMode;
			HandleCameraKey,            // CameraKey *cameraKey // DOUBLE CHECK THIS
			HandleCameraTimer,          // long cameraTimer;
			HandleCameraSmooth,         // long cameraSmooth;
			HandleCameraValue,          // long index; long value;
			HandleCameraLock,           // long cameraLock;
			HandleCameraUnlock,         // long cameraUnlock;
			HandleCameraSave,           // long cameraSave;
			HandleCameraRestore,        // long cameraRestore;
			HandleSetMirror = 42,       // Mirror *mirror;
			HandleFogNear,              // long fogNear;
			HandleFogFar,               // long fogFar;
			HandleCameraShake = 48,     // long time; long scale;
			HandleCallSignal = 54,      // void* callSignal; (callSignal is itself a signal?)
			HandleEnd = 60,             // (nothing)
			HandleStopPlayerControl = 62,    // (nothing)
			HandleStartPlayerControl,   // (nothing)
			HandleStreamLevel = 78,     // long currentnum; long streamID; char toname[16];
			HandleCameraSpline = 80,    // long index; Intro* intro;
			HandleScreenWipe,           // short type; short time;
			HandleBlendStart = 93,      // long blendStart;
			HandleScreenWipeColor = 98, // unsigned char r; unsigned char g; unsigned char b; unsigned char pad;
			HandleSetSlideAngle = 106,  // long slideAngle;
			HandleResetSlideAngle,      // (nothing)
			HandleSetCameraTilt,        // long cameraTilt;
			HandleSetCameraDistance,    // long cameraDistance;
		}

		public enum SignalTypeFeb16
		{
			HandleHideObject = 0,       // Intro* intro;
			HandleUnhideObject,         // Intro* intro;
			HandleDecoupledTimer,       // long timer;
			HandleGotoFrame,            // Intro* intro; long frame;
			HandleChangeModel,          // Intro* intro; long model;
			HandleStartAniTex,
			HandleStopAniTex,
			HandleStartSpline,
			HandleStopSpline,
			HandleDeathZ,
			HandleDSignal,
			HandleGSignal,
			HandleLightGroup,           // long lightGroup;
			HandleCameraAdjust,         // long cameraAdjust;
			HandleCameraMode,           // long cameraMode;
			HandleCameraKey,            // CameraKey *cameraKey // DOUBLE CHECK THIS
			HandleCameraTimer,          // long cameraTimer;
			HandleCameraSmooth,         // long cameraSmooth;
			HandleCameraValue,          // long index; long value;
			HandleCameraLock,           // long cameraLock;
			HandleCameraUnlock,         // long cameraUnlock;
			HandleCameraSave,           // long cameraSave;
			HandleCameraRestore,        // long cameraRestore;
			HandleTeleport,
			HandleFarPlane,
			HandleSoundStartSequence,
			HandleSoundStopSlot,
			HandleSoundPauseSlot,
			HandleSoundResumeSlot,
			HandleSoundMuteChannel,
			HandleSoundUnmuteChannel,
			HandleTimes,
			HandleFreeze,
			HandleUnfreeze,
			HandleFreezeAll,
			HandleUnfreezeAll,
			HandleHideBG,
			HandleUnhideBG,
			HandleHideBGObject,
			HandleUnhideBGObject,
			HandleMirror,
			HandleUnmirror,
			HandleSetMirror,            // Mirror* mirror;
			HandleFogNear,              // long fogNear;
			HandleFogFar,               // long fogFar;
			HandleStartVertexMorph,     // VMObject* vmobject
			HandleStopVertexMorph,      // VMObject* vmobject
			HandleLogicValue,
			HandleCameraShake,          // long time; long scale;
			HandleLogicAnd,
			HandleLogicOr,
			HandleLogicXor,
			HandleLogicTrue,
			HandleLogicFalse,
			HandleCallSignal,           // void* callSignal; (callSignal is itself a signal?)
			HandleOffset,
			HandleLogicAdd,
			HandleLogicSub,
			HandleGoto,
			HandleLabel,
			HandleEnd,                  // (nothing)
			HandleGosub,
			HandleStopPlayerControl,    // (nothing)
			HandleStartPlayerControl,   // (nothing)
			HandleSetPlayerControl,
			HandleLaunch,
			HandleCostumeChange,
			HandleGameValue,
			HandleSetZSignal,
			HandleResetZSignal,
			HandleSoundEffect,
			HandleMusicControl,
			HandleLevelChange,
			HandleVoiceControl,
			HandleBankChange,
			HandleGameLoadSave,
			HandleHideObjectGroup,
			HandleUnhideObjectGroup,
			HandleStreamLevel,          // long currentnum; long streamID; char toname[16];
			HandleShards,
			HandleCameraSpline,         // long index; Intro* intro;
			HandleScreenWipe,           // short type; short time;
			HandleVoiceQueue,
			HandleVoiceUnQueue,
			HandleVoiceRequest,
			HandleVoiceClearQueue,
			HandleVoiceForce,
			HandleSetMusicVariable,
			HandleIntroActive,
			HandleIntroFX,
			HandleGotoPos,
			HandleFrameTimer,
			HandleBirthObject,
			HandleBlendStart,           // long blendStart;
			HandleMiscValue,
			HandleVoiceDisable,
			HandleVoiceReEnable,
			HandleSetTimes,
			HandleScreenWipeColor,      // unsigned char r; unsigned char g; unsigned char b; unsigned char pad;
			HandleRelocate,
			HandleLogicTrueElse,
			HandleLogicFalseElse,
			HandlePrint,
			HandleTagTimer,
			HandleSetNoRemove,
			HandleResetNoRemove,
			HandleSetSlideAngle,        // long slideAngle;
			HandleResetSlideAngle,      // (nothing)
			HandleSetCameraTilt,        // long cameraTilt;
			HandleSetCameraDistance,    // long cameraDistance;
			HandleDecoupledTimer2,
		}

		public readonly SR1_Primative<int> id = new SR1_Primative<int>();
		public SignalData data = new SignalData();

		public bool OmitFromMigration { get; set; } = false;

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			id.Read(reader, this, "id");

			if (reader.File._Version >= SR1_File.Version.Jun01)
			{
				switch ((SignalTypeJun01)id.Value)
				{
					case SignalTypeJun01.HandleLightGroup:
						data = new SignalLightGroup();
						break;
					case SignalTypeJun01.HandleCameraAdjust:
						data = new SignalCameraAdjust();
						break;
					case SignalTypeJun01.HandleCameraMode:
						data = new SignalCameraMode();
						break;
					case SignalTypeJun01.HandleCameraKey:
						data = new SignalCameraKey();
						break;
					case SignalTypeJun01.HandleCameraTimer:
						data = new SignalCameraTimer();
						break;
					case SignalTypeJun01.HandleCameraSmooth:
						data = new SignalCameraSmooth();
						break;
					case SignalTypeJun01.HandleCameraValue:
						data = new SignalCameraValue();
						break;
					case SignalTypeJun01.HandleCameraLock:
						data = new SignalCameraLock();
						break;
					case SignalTypeJun01.HandleCameraUnlock:
						data = new SignalCameraUnlock();
						break;
					case SignalTypeJun01.HandleCameraSave:
						data = new SignalCameraSave();
						break;
					case SignalTypeJun01.HandleCameraRestore:
						data = new SignalCameraRestore();
						break;
					case SignalTypeJun01.HandleFogNear:
						data = new SignalFogNear();
						break;
					case SignalTypeJun01.HandleFogFar:
						data = new SignalFogFar();
						break;
					case SignalTypeJun01.HandleCameraShake:
						data = new SignalCameraShake();
						break;
					case SignalTypeJun01.HandleCallSignal:
						data = new SignalCallSignal();
						break;
					case SignalTypeJun01.HandleEnd:
						data = new SignalEnd();
						break;
					case SignalTypeJun01.HandleStopPlayerControl:
						data = new SignalStopPlayerControl();
						break;
					case SignalTypeJun01.HandleStartPlayerControl:
						data = new SignalStartPlayerControl();
						break;
					case SignalTypeJun01.HandleStreamLevel:
						data = new SignalStreamLevel();
						break;
					case SignalTypeJun01.HandleCameraSpline:
						data = new SignalCameraSpline();
						break;
					case SignalTypeJun01.HandleScreenWipe:
						data = new SignalScreenWipe();
						break;
					case SignalTypeJun01.HandleBlendStart:
						data = new SignalBlendStart();
						break;
					case SignalTypeJun01.HandleScreenWipeColor:
						data = new SignalScreenWipeColor();
						break;
					case SignalTypeJun01.HandleSetSlideAngle:
						data = new SignalSetSlideAngle();
						break;
					case SignalTypeJun01.HandleResetSlideAngle:
						data = new SignalResetSlideAngle();
						break;
					case SignalTypeJun01.HandleSetCameraTilt:
						data = new SignalCameraTilt();
						break;
					case SignalTypeJun01.HandleSetCameraDistance:
						data = new SignalCameraDistance();
						break;
					default:
						// The lengths can be different. Check the length field in signalInfoList entries.
						data = new SignalDepricated(GetSizeOfDepricated(reader, id.Value));
						break;
				}
			}
			else if (reader.File._Version >= SR1_File.Version.Apr14)
			{
				switch ((SignalTypeMay12)id.Value)
				{
					case SignalTypeMay12.HandleLightGroup:
						data = new SignalLightGroup();
						break;
					case SignalTypeMay12.HandleCameraAdjust:
						data = new SignalCameraAdjust();
						break;
					case SignalTypeMay12.HandleCameraMode:
						data = new SignalCameraMode();
						break;
					case SignalTypeMay12.HandleCameraKey:
						data = new SignalCameraKey();
						break;
					case SignalTypeMay12.HandleCameraTimer:
						data = new SignalCameraTimer();
						break;
					case SignalTypeMay12.HandleCameraSmooth:
						data = new SignalCameraSmooth();
						break;
					case SignalTypeMay12.HandleCameraValue:
						data = new SignalCameraValue();
						break;
					case SignalTypeMay12.HandleCameraLock:
						data = new SignalCameraLock();
						break;
					case SignalTypeMay12.HandleCameraUnlock:
						data = new SignalCameraUnlock();
						break;
					case SignalTypeMay12.HandleCameraSave:
						data = new SignalCameraSave();
						break;
					case SignalTypeMay12.HandleCameraRestore:
						data = new SignalCameraRestore();
						break;
					case SignalTypeMay12.HandleSetMirror:
						data = new SignalSetMirror();
						break;
					case SignalTypeMay12.HandleFogNear:
						data = new SignalFogNear();
						break;
					case SignalTypeMay12.HandleFogFar:
						data = new SignalFogFar();
						break;
					case SignalTypeMay12.HandleCameraShake:
						data = new SignalCameraShake();
						break;
					case SignalTypeMay12.HandleCallSignal:
						data = new SignalCallSignal();
						break;
					case SignalTypeMay12.HandleEnd:
						data = new SignalEnd();
						break;
					case SignalTypeMay12.HandleStopPlayerControl:
						data = new SignalStopPlayerControl();
						break;
					case SignalTypeMay12.HandleStartPlayerControl:
						data = new SignalStartPlayerControl();
						break;
					case SignalTypeMay12.HandleStreamLevel:
						data = new SignalStreamLevel();
						break;
					case SignalTypeMay12.HandleCameraSpline:
						data = new SignalCameraSpline();
						break;
					case SignalTypeMay12.HandleScreenWipe:
						data = new SignalScreenWipe();
						break;
					case SignalTypeMay12.HandleBlendStart:
						data = new SignalBlendStart();
						break;
					case SignalTypeMay12.HandleScreenWipeColor:
						data = new SignalScreenWipeColor();
						break;
					case SignalTypeMay12.HandleSetSlideAngle:
						data = new SignalSetSlideAngle();
						break;
					case SignalTypeMay12.HandleResetSlideAngle:
						data = new SignalResetSlideAngle();
						break;
					case SignalTypeMay12.HandleSetCameraTilt:
						data = new SignalCameraTilt();
						break;
					case SignalTypeMay12.HandleSetCameraDistance:
						data = new SignalCameraDistance();
						break;
					default:
						// The lengths can be different. Check the length field in signalInfoList entries.
						data = new SignalDepricated(GetSizeOfDepricated(reader, id.Value));
						break;
				}
			}
			else if (reader.File._Version >= SR1_File.Version.Feb04)
			{
				switch ((SignalTypeFeb16)id.Value)
				{
					case SignalTypeFeb16.HandleHideObject:
						data = new SignalHideObject();
						break;
					case SignalTypeFeb16.HandleUnhideObject:
						data = new SignalHideObject();
						break;
					case SignalTypeFeb16.HandleDecoupledTimer:
						data = new SignalDecoupledTimer();
						break;
					case SignalTypeFeb16.HandleGotoFrame:
						data = new SignalGoToFrame();
						break;
					case SignalTypeFeb16.HandleChangeModel:
						data = new SignalChangeModel();
						break;
					case SignalTypeFeb16.HandleStartAniTex:
						data = new SignalStartAnitex();
						break;
					case SignalTypeFeb16.HandleStopAniTex:
						data = new SignalStopAnitex();
						break;
					case SignalTypeFeb16.HandleStartSpline:
						data = new SignalStartSpline();
						break;
					case SignalTypeFeb16.HandleStopSpline:
						data = new SignalStopSpline();
						break;
					case SignalTypeFeb16.HandleDeathZ:
						data = new SignalDeathZ();
						break;
					case SignalTypeFeb16.HandleDSignal:
						data = new SignalDSignal();
						break;
					case SignalTypeFeb16.HandleGSignal:
						data = new SignalGsignal();
						break;
					case SignalTypeFeb16.HandleLightGroup:
						data = new SignalLightGroup();
						break;
					case SignalTypeFeb16.HandleCameraAdjust:
						data = new SignalCameraAdjust();
						break;
					case SignalTypeFeb16.HandleCameraMode:
						data = new SignalCameraMode();
						break;
					case SignalTypeFeb16.HandleCameraKey:
						data = new SignalCameraKey();
						break;
					case SignalTypeFeb16.HandleCameraTimer:
						data = new SignalCameraTimer();
						break;
					case SignalTypeFeb16.HandleCameraSmooth:
						data = new SignalCameraSmooth();
						break;
					case SignalTypeFeb16.HandleCameraValue:
						data = new SignalCameraValue();
						break;
					case SignalTypeFeb16.HandleCameraLock:
						data = new SignalCameraLock();
						break;
					case SignalTypeFeb16.HandleCameraUnlock:
						data = new SignalCameraUnlock();
						break;
					case SignalTypeFeb16.HandleCameraSave:
						data = new SignalCameraSave();
						break;
					case SignalTypeFeb16.HandleCameraRestore:
						data = new SignalCameraRestore();
						break;
					case SignalTypeFeb16.HandleSoundStartSequence:
						data = new SignalSoundStartSequence();
						break;
					case SignalTypeFeb16.HandleMirror:
						data = new SignalMirror();
						break;
					case SignalTypeFeb16.HandleUnmirror:
						data = new SignalUnmirror();
						break;
					case SignalTypeFeb16.HandleSetMirror:
						data = new SignalSetMirror();
						break;
					case SignalTypeFeb16.HandleFogNear:
						data = new SignalFogNear();
						break;
					case SignalTypeFeb16.HandleFogFar:
						data = new SignalFogFar();
						break;
					case SignalTypeFeb16.HandleStartVertexMorph:
						data = new SignalStartVertexMorph();
						break;
					case SignalTypeFeb16.HandleStopVertexMorph:
						data = new SignalStopVertexMorph();
						break;
					case SignalTypeFeb16.HandleCameraShake:
						data = new SignalCameraShake();
						break;
					case SignalTypeFeb16.HandleCallSignal:
						data = new SignalCallSignal();
						break;
					case SignalTypeFeb16.HandleEnd:
						data = new SignalEnd();
						break;
					case SignalTypeFeb16.HandleStopPlayerControl:
						data = new SignalStopPlayerControl();
						break;
					case SignalTypeFeb16.HandleStartPlayerControl:
						data = new SignalStartPlayerControl();
						break;
					case SignalTypeFeb16.HandleStreamLevel:
						data = new SignalStreamLevel();
						break;
					case SignalTypeFeb16.HandleCameraSpline:
						data = new SignalCameraSpline();
						break;
					case SignalTypeFeb16.HandleScreenWipe:
						data = new SignalScreenWipe();
						break;
					case SignalTypeFeb16.HandleBlendStart:
						data = new SignalBlendStart();
						break;
					case SignalTypeFeb16.HandleScreenWipeColor:
						data = new SignalScreenWipeColor();
						break;
					case SignalTypeFeb16.HandleSetSlideAngle:
						data = new SignalSetSlideAngle();
						break;
					case SignalTypeFeb16.HandleResetSlideAngle:
						data = new SignalResetSlideAngle();
						break;
					case SignalTypeFeb16.HandleSetCameraTilt:
						data = new SignalCameraTilt();
						break;
					case SignalTypeFeb16.HandleSetCameraDistance:
						data = new SignalCameraDistance();
						break;
					default:
						// The lengths can be different. Check the length field in signalInfoList entries.
						data = new SignalDepricated(GetSizeOfDepricated(reader, id.Value));
						break;
				}
			}

			data.SetPadding(4).Read(reader, this, "data");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			id.Write(writer);
			data.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				int newID = -1;

				switch ((SignalTypeFeb16)id.Value)
				{
					case SignalTypeFeb16.HandleLightGroup: newID = (int)SignalTypeJun01.HandleLightGroup; break;
					case SignalTypeFeb16.HandleCameraAdjust: newID = (int)SignalTypeJun01.HandleCameraAdjust; break;
					case SignalTypeFeb16.HandleCameraMode: newID = (int)SignalTypeJun01.HandleCameraMode; break;
					case SignalTypeFeb16.HandleCameraKey: newID = (int)SignalTypeJun01.HandleCameraKey; break;
					case SignalTypeFeb16.HandleCameraTimer: newID = (int)SignalTypeJun01.HandleCameraTimer; break;
					case SignalTypeFeb16.HandleCameraSmooth: newID = (int)SignalTypeJun01.HandleCameraSmooth; break;
					case SignalTypeFeb16.HandleCameraValue: newID = (int)SignalTypeJun01.HandleCameraValue; break;
					case SignalTypeFeb16.HandleCameraLock: newID = (int)SignalTypeJun01.HandleCameraLock; break;
					case SignalTypeFeb16.HandleCameraUnlock: newID = (int)SignalTypeJun01.HandleCameraUnlock; break;
					case SignalTypeFeb16.HandleCameraSave: newID = (int)SignalTypeJun01.HandleCameraSave; break;
					case SignalTypeFeb16.HandleCameraRestore: newID = (int)SignalTypeJun01.HandleCameraRestore; break;
					case SignalTypeFeb16.HandleFogNear: newID = (int)SignalTypeJun01.HandleFogNear; break;
					case SignalTypeFeb16.HandleFogFar: newID = (int)SignalTypeJun01.HandleFogFar; break;
					case SignalTypeFeb16.HandleCameraShake: newID = (int)SignalTypeJun01.HandleCameraShake; break;
					case SignalTypeFeb16.HandleCallSignal: newID = (int)SignalTypeJun01.HandleCallSignal; break;
					case SignalTypeFeb16.HandleEnd: newID = (int)SignalTypeJun01.HandleEnd; break;
					case SignalTypeFeb16.HandleStopPlayerControl: newID = (int)SignalTypeJun01.HandleStopPlayerControl; break;
					case SignalTypeFeb16.HandleStartPlayerControl: newID = (int)SignalTypeJun01.HandleStartPlayerControl; break;
					case SignalTypeFeb16.HandleStreamLevel: newID = (int)SignalTypeJun01.HandleStreamLevel; break;
					case SignalTypeFeb16.HandleCameraSpline: newID = (int)SignalTypeJun01.HandleCameraSpline; break;
					case SignalTypeFeb16.HandleScreenWipe: newID = (int)SignalTypeJun01.HandleScreenWipe; break;
					case SignalTypeFeb16.HandleBlendStart: newID = (int)SignalTypeJun01.HandleBlendStart; break;
					case SignalTypeFeb16.HandleScreenWipeColor: newID = (int)SignalTypeJun01.HandleScreenWipeColor; break;
					case SignalTypeFeb16.HandleSetSlideAngle: newID = (int)SignalTypeJun01.HandleSetSlideAngle; break;
					case SignalTypeFeb16.HandleResetSlideAngle: newID = (int)SignalTypeJun01.HandleResetSlideAngle; break;
					case SignalTypeFeb16.HandleSetCameraTilt: newID = (int)SignalTypeJun01.HandleSetCameraTilt; break;
					case SignalTypeFeb16.HandleSetCameraDistance: newID = (int)SignalTypeJun01.HandleSetCameraDistance; break;
				}

				id.Value = newID;

				if ((migrateFlags & SR1_File.MigrateFlags.RemoveSignals) != 0 || id.Value < 0)
				{
					OmitFromMigration = true;
				}
			}
		}

		int GetSizeOfDepricated(SR1_Reader reader, int id)
		{
			if (reader.File._Version >= SR1_File.Version.Jun01)
			{
				switch (id)
				{
					case 41:
						return 2;
					default:
						return 1;
				}
			}
			else if (reader.File._Version >= SR1_File.Version.Apr14)
			{
				switch (id)
				{
					case 4:
					case 10:
					case 25:
					case 29:
					case 30:
					case 47:
					case 52:
					case 53:
					case 55:
					case 64:
					case 65:
					case 87:
					case 89:
					case 90:
					case 94:
					case 97:
					case 101:
					case 102:
						return 2;
					case 79:
					case 103:
						return 3;
					case 23:
					case 99:
						return 4;
					case 34:
					case 35:
					case 36:
					case 37:
					case 40:
					case 41:
					case 59:
					case 61:
					case 69:
					case 70:
					case 85:
					case 95:
					case 96:
					case 104:
						return 0;
					default:
						return 1;
				}
			}
			else if (reader.File._Version >= SR1_File.Version.Feb04)
			{
				switch (id)
				{
					default:
						return 1;
				}
			}
			return 1;
		}
	}
}