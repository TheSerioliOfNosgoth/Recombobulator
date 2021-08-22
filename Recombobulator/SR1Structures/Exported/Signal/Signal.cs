using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Signal : SR1_Structure
    {
        private enum SignalTypeRetail
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
        private enum SignalTypeBeta
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
            HandleMirror = 42,          // Mirror *mirror;
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

        SR1_Primative<int> id = new SR1_Primative<int>();
        SignalData data = new SignalData();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            id.Read(reader, this, "id");

            if (reader.File._Version >= SR1_File.Version.Jun01)
            {
                switch ((SignalTypeRetail)id.Value)
                {
                    case SignalTypeRetail.HandleLightGroup:
                        data = new SignalLightGroup();
                        break;
                    case SignalTypeRetail.HandleCameraAdjust:
                        data = new SignalCameraAdjust();
                        break;
                    case SignalTypeRetail.HandleCameraMode:
                        data = new SignalCameraMode();
                        break;
                    case SignalTypeRetail.HandleCameraKey:
                        data = new SignalCameraKey();
                        break;
                    case SignalTypeRetail.HandleCameraTimer:
                        data = new SignalCameraTimer();
                        break;
                    case SignalTypeRetail.HandleCameraSmooth:
                        data = new SignalCameraSmooth();
                        break;
                    case SignalTypeRetail.HandleCameraValue:
                        data = new SignalCameraValue();
                        break;
                    case SignalTypeRetail.HandleCameraLock:
                        data = new SignalCameraLock();
                        break;
                    case SignalTypeRetail.HandleCameraUnlock:
                        data = new SignalCameraUnlock();
                        break;
                    case SignalTypeRetail.HandleCameraSave:
                        data = new SignalCameraSave();
                        break;
                    case SignalTypeRetail.HandleCameraRestore:
                        data = new SignalCameraRestore();
                        break;
                    case SignalTypeRetail.HandleFogNear:
                        data = new SignalFogNear();
                        break;
                    case SignalTypeRetail.HandleFogFar:
                        data = new SignalFogFar();
                        break;
                    case SignalTypeRetail.HandleCameraShake:
                        data = new SignalCameraShake();
                        break;
                    case SignalTypeRetail.HandleCallSignal:
                        data = new SignalCallSignal();
                        break;
                    case SignalTypeRetail.HandleEnd:
                        data = new SignalEnd();
                        break;
                    case SignalTypeRetail.HandleStopPlayerControl:
                        data = new SignalStopPlayerControl();
                        break;
                    case SignalTypeRetail.HandleStartPlayerControl:
                        data = new SignalStartPlayerControl();
                        break;
                    case SignalTypeRetail.HandleStreamLevel:
                        data = new SignalStreamLevel();
                        break;
                    case SignalTypeRetail.HandleCameraSpline:
                        data = new SignalCameraSpline();
                        break;
                    case SignalTypeRetail.HandleScreenWipe:
                        data = new SignalScreenWipe();
                        break;
                    case SignalTypeRetail.HandleBlendStart:
                        data = new SignalBlendStart();
                        break;
                    case SignalTypeRetail.HandleScreenWipeColor:
                        data = new SignalScreenWipeColor();
                        break;
                    case SignalTypeRetail.HandleSetSlideAngle:
                        data = new SignalSetSlideAngle();
                        break;
                    case SignalTypeRetail.HandleResetSlideAngle:
                        data = new SignalResetSlideAngle();
                        break;
                    case SignalTypeRetail.HandleSetCameraTilt:
                        data = new SignalCameraTilt();
                        break;
                    case SignalTypeRetail.HandleSetCameraDistance:
                        data = new SignalCameraDistance();
                        break;
                    default:
                        // The lenghs can be different. Check the length field in signalInfoList entries.
                        data = new SignalDepricated(GetSizeOfDepricated(reader, id.Value));
                        break;
                }
            }
            else if (reader.File._Version >= SR1_File.Version.May12)
            {
                switch ((SignalTypeBeta)id.Value)
                {
                    case SignalTypeBeta.HandleLightGroup:
                        data = new SignalLightGroup();
                        break;
                    case SignalTypeBeta.HandleCameraAdjust:
                        data = new SignalCameraAdjust();
                        break;
                    case SignalTypeBeta.HandleCameraMode:
                        data = new SignalCameraMode();
                        break;
                    case SignalTypeBeta.HandleCameraKey:
                        data = new SignalCameraKey();
                        break;
                    case SignalTypeBeta.HandleCameraTimer:
                        data = new SignalCameraTimer();
                        break;
                    case SignalTypeBeta.HandleCameraSmooth:
                        data = new SignalCameraSmooth();
                        break;
                    case SignalTypeBeta.HandleCameraValue:
                        data = new SignalCameraValue();
                        break;
                    case SignalTypeBeta.HandleCameraLock:
                        data = new SignalCameraLock();
                        break;
                    case SignalTypeBeta.HandleCameraUnlock:
                        data = new SignalCameraUnlock();
                        break;
                    case SignalTypeBeta.HandleCameraSave:
                        data = new SignalCameraSave();
                        break;
                    case SignalTypeBeta.HandleCameraRestore:
                        data = new SignalCameraRestore();
                        break;
                    case SignalTypeBeta.HandleMirror:
                        data = new SignalMirror();
                        break;
                    case SignalTypeBeta.HandleFogNear:
                        data = new SignalFogNear();
                        break;
                    case SignalTypeBeta.HandleFogFar:
                        data = new SignalFogFar();
                        break;
                    case SignalTypeBeta.HandleCameraShake:
                        data = new SignalCameraShake();
                        break;
                    case SignalTypeBeta.HandleCallSignal:
                        data = new SignalCallSignal();
                        break;
                    case SignalTypeBeta.HandleEnd:
                        data = new SignalEnd();
                        break;
                    case SignalTypeBeta.HandleStopPlayerControl:
                        data = new SignalStopPlayerControl();
                        break;
                    case SignalTypeBeta.HandleStartPlayerControl:
                        data = new SignalStartPlayerControl();
                        break;
                    case SignalTypeBeta.HandleStreamLevel:
                        data = new SignalStreamLevel();
                        break;
                    case SignalTypeBeta.HandleCameraSpline:
                        data = new SignalCameraSpline();
                        break;
                    case SignalTypeBeta.HandleScreenWipe:
                        data = new SignalScreenWipe();
                        break;
                    case SignalTypeBeta.HandleBlendStart:
                        data = new SignalBlendStart();
                        break;
                    case SignalTypeBeta.HandleScreenWipeColor:
                        data = new SignalScreenWipeColor();
                        break;
                    case SignalTypeBeta.HandleSetSlideAngle:
                        data = new SignalSetSlideAngle();
                        break;
                    case SignalTypeBeta.HandleResetSlideAngle:
                        data = new SignalResetSlideAngle();
                        break;
                    case SignalTypeBeta.HandleSetCameraTilt:
                        data = new SignalCameraTilt();
                        break;
                    case SignalTypeBeta.HandleSetCameraDistance:
                        data = new SignalCameraDistance();
                        break;
                    default:
                        // The lenghs can be different. Check the length field in signalInfoList entries.
                        data = new SignalDepricated(GetSizeOfDepricated(reader, id.Value));
                        break;
                }
            }

            data.Read(reader, this, "data");

            while (((uint)reader.BaseStream.Position - Start) % 4u != 0)
            {
                reader.BaseStream.Position++;
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            id.Write(writer);
            data.Write(writer);
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
            else if (reader.File._Version >= SR1_File.Version.May12)
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

            return 1;
        }
    }
}