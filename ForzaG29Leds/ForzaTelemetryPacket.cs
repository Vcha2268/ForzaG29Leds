using System.Runtime.InteropServices;

namespace ForzaG29Leds;

// Forza Horizon 5 / Forza Horizon 6  "Car Dash" UDP packet — 324 bytes.
//
// Enable in-game:  Settings > HUD and Gameplay > Data Out
//   Data Out Type : Car Dash
//   Data Out IP   : 127.0.0.1
//   Data Out Port : 9999
//
// Layout (confirmed against https://github.com/TheBanHammer/fh6-tel):
//   0–231   Sled block (identical to ForzaMotorsport 7)
//   232–243 Unknown / reserved (12 bytes, always zero in practice)
//   244–255 World position (X/Y/Z)
//   256–322 Car Dash extension (speed, power, temps, inputs …)
//   323     FH6 TrackOrdinal (1 byte)
//
// Note: tire temperatures are transmitted in Fahrenheit.
[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 324)]
public struct ForzaTelemetryPacket
{
    // ── Sled (0–231) ──────────────────────────────────────────────────────────

    [FieldOffset(0)] public int IsRaceOn;           // 0 = in menus / paused
    [FieldOffset(4)] public uint TimestampMS;

    [FieldOffset(8)] public float EngineMaxRpm;        // redline RPM
    [FieldOffset(12)] public float EngineIdleRpm;
    [FieldOffset(16)] public float CurrentEngineRpm;

    [FieldOffset(20)] public float AccelerationX;
    [FieldOffset(24)] public float AccelerationY;
    [FieldOffset(28)] public float AccelerationZ;

    [FieldOffset(32)] public float VelocityX;
    [FieldOffset(36)] public float VelocityY;
    [FieldOffset(40)] public float VelocityZ;

    [FieldOffset(44)] public float AngularVelocityX;
    [FieldOffset(48)] public float AngularVelocityY;
    [FieldOffset(52)] public float AngularVelocityZ;

    [FieldOffset(56)] public float Yaw;
    [FieldOffset(60)] public float Pitch;
    [FieldOffset(64)] public float Roll;

    [FieldOffset(68)] public float NormSuspTravelFl;
    [FieldOffset(72)] public float NormSuspTravelFr;
    [FieldOffset(76)] public float NormSuspTravelRl;
    [FieldOffset(80)] public float NormSuspTravelRr;

    [FieldOffset(84)] public float TireSlipRatioFl;
    [FieldOffset(88)] public float TireSlipRatioFr;
    [FieldOffset(92)] public float TireSlipRatioRl;
    [FieldOffset(96)] public float TireSlipRatioRr;

    [FieldOffset(100)] public float WheelRotationSpeedFl;
    [FieldOffset(104)] public float WheelRotationSpeedFr;
    [FieldOffset(108)] public float WheelRotationSpeedRl;
    [FieldOffset(112)] public float WheelRotationSpeedRr;

    [FieldOffset(116)] public int WheelOnRumbleStripFl;
    [FieldOffset(120)] public int WheelOnRumbleStripFr;
    [FieldOffset(124)] public int WheelOnRumbleStripRl;
    [FieldOffset(128)] public int WheelOnRumbleStripRr;

    [FieldOffset(132)] public float WheelInPuddleDepthFl;
    [FieldOffset(136)] public float WheelInPuddleDepthFr;
    [FieldOffset(140)] public float WheelInPuddleDepthRl;
    [FieldOffset(144)] public float WheelInPuddleDepthRr;

    [FieldOffset(148)] public float SurfaceRumbleFl;
    [FieldOffset(152)] public float SurfaceRumbleFr;
    [FieldOffset(156)] public float SurfaceRumbleRl;
    [FieldOffset(160)] public float SurfaceRumbleRr;

    [FieldOffset(164)] public float TireSlipAngleFl;
    [FieldOffset(168)] public float TireSlipAngleFr;
    [FieldOffset(172)] public float TireSlipAngleRl;
    [FieldOffset(176)] public float TireSlipAngleRr;

    [FieldOffset(180)] public float TireCombinedSlipFl;
    [FieldOffset(184)] public float TireCombinedSlipFr;
    [FieldOffset(188)] public float TireCombinedSlipRl;
    [FieldOffset(192)] public float TireCombinedSlipRr;

    [FieldOffset(196)] public float SuspensionTravelMetersFl;
    [FieldOffset(200)] public float SuspensionTravelMetersFr;
    [FieldOffset(204)] public float SuspensionTravelMetersRl;
    [FieldOffset(208)] public float SuspensionTravelMetersRr;

    [FieldOffset(212)] public int CarOrdinal;
    [FieldOffset(216)] public int CarClass;           // 0=D … 6=X
    [FieldOffset(220)] public int CarPerformanceIndex;
    [FieldOffset(224)] public int DrivetrainType;     // 0=FWD 1=RWD 2=AWD
    [FieldOffset(228)] public int NumCylinders;

    // ── Unknown / reserved (232–243, 12 bytes) ────────────────────────────────
    // Consistently zero in practice; purpose undocumented.

    // ── World position (244–255) ──────────────────────────────────────────────

    [FieldOffset(244)] public float PositionX;
    [FieldOffset(248)] public float PositionY;
    [FieldOffset(252)] public float PositionZ;

    // ── Car Dash extension (256–322) ──────────────────────────────────────────

    [FieldOffset(256)] public float Speed;              // m/s
    [FieldOffset(260)] public float Power;              // watts
    [FieldOffset(264)] public float Torque;             // N·m

    // Temperatures are transmitted in Fahrenheit
    [FieldOffset(268)] public float TireTempFlF;
    [FieldOffset(272)] public float TireTempFrF;
    [FieldOffset(276)] public float TireTempRlF;
    [FieldOffset(280)] public float TireTempRrF;

    [FieldOffset(284)] public float Boost;
    [FieldOffset(288)] public float Fuel;               // 0.0–1.0
    [FieldOffset(292)] public float DistanceTraveled;
    [FieldOffset(296)] public float BestLap;
    [FieldOffset(300)] public float LastLap;
    [FieldOffset(304)] public float CurrentLap;
    [FieldOffset(308)] public float CurrentRaceTime;

    [FieldOffset(312)] public ushort LapNumber;
    [FieldOffset(314)] public byte RacePosition;
    [FieldOffset(315)] public byte Accel;
    [FieldOffset(316)] public byte Brake;
    [FieldOffset(317)] public byte Clutch;
    [FieldOffset(318)] public byte HandBrake;
    [FieldOffset(319)] public byte Gear;
    [FieldOffset(320)] public sbyte Steer;
    [FieldOffset(321)] public sbyte NormalizedDrivingLine;
    [FieldOffset(322)] public sbyte NormalizedAIBrakeDifference;

    // ── FH6 tail (323) ────────────────────────────────────────────────────────

    [FieldOffset(323)] public byte TrackOrdinal;

    // Helpers
    public float TireTempFlC => (TireTempFlF - 32f) * 5f / 9f;
    public float TireTempFrC => (TireTempFrF - 32f) * 5f / 9f;
    public float TireTempRlC => (TireTempRlF - 32f) * 5f / 9f;
    public float TireTempRrC => (TireTempRrF - 32f) * 5f / 9f;
}
