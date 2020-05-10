using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Signal : SR1_Structure
    {
        private enum SignalType
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

        SR1_Primative<int> id = new SR1_Primative<int>();
        SignalData data = new SignalData();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            id.Read(reader, this, "id");

            switch ((SignalType)id.Value)
            {
                case SignalType.HandleLightGroup:
                    data = new SignalLightGroup();
                    break;
                case SignalType.HandleCameraAdjust:
                    data = new SignalCameraAdjust();
                    break;
                case SignalType.HandleCameraMode:
                    data = new SignalCameraMode();
                    break;
                case SignalType.HandleCameraKey:
                    data = new SignalCameraKey();
                    break;
                case SignalType.HandleCameraTimer:
                    data = new SignalCameraTimer();
                    break;
                case SignalType.HandleCameraSmooth:
                    data = new SignalCameraSmooth();
                    break;
                case SignalType.HandleCameraValue:
                    data = new SignalCameraValue();
                    break;
                case SignalType.HandleCameraLock:
                    data = new SignalCameraLock();
                    break;
                case SignalType.HandleCameraUnlock:
                    data = new SignalCameraUnlock();
                    break;
                case SignalType.HandleCameraSave:
                    data = new SignalCameraSave();
                    break;
                case SignalType.HandleCameraRestore:
                    data = new SignalCameraRestore();
                    break;
                case SignalType.HandleFogNear:
                    data = new SignalFogNear();
                    break;
                case SignalType.HandleFogFar:
                    data = new SignalFogFar();
                    break;
                case SignalType.HandleCameraShake:
                    data = new SignalCameraShake();
                    break;
                case SignalType.HandleCallSignal:
                    data = new SignalCallSignal();
                    break;
                case SignalType.HandleEnd:
                    data = new SignalEnd();
                    break;
                case SignalType.HandleStopPlayerControl:
                    data = new SignalStopPlayerControl();
                    break;
                case SignalType.HandleStartPlayerControl:
                    data = new SignalStartPlayerControl();
                    break;
                case SignalType.HandleStreamLevel:
                    data = new SignalStreamLevel();
                    break;
                case SignalType.HandleCameraSpline:
                    data = new SignalCameraSpline();
                    break;
                case SignalType.HandleScreenWipe:
                    data = new SignalScreenWipe();
                    break;
                case SignalType.HandleBlendStart:
                    data = new SignalBlendStart();
                    break;
                case SignalType.HandleScreenWipeColor:
                    data = new SignalScreenWipeColor();
                    break;
                case SignalType.HandleSetSlideAngle:
                    data = new SignalSetSlideAngle();
                    break;
                case SignalType.HandleResetSlideAngle:
                    data = new SignalResetSlideAngle();
                    break;
                case SignalType.HandleSetCameraTilt:
                    data = new SignalCameraTilt();
                    break;
                case SignalType.HandleSetCameraDistance:
                    data = new SignalCameraDistance();
                    break;
                default:
                    data = new SignalData();
                    break;
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
    }
}