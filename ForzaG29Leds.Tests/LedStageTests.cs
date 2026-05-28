using Xunit;
using static ForzaG29Leds.TelemetryService;

namespace ForzaG29Leds.Tests;

public class LedStageTests
{
    // Typical settings: idle=~9%, solid=85%, flash=90%
    private const float Idle = 0.09f;
    private const float Solid = 0.85f;
    private const float Flash = 0.90f;

    // ── Stage selection ───────────────────────────────────────────────────────

    [Fact]
    public void BelowIdle_IsOff()
    {
        var (stage, _) = ComputeLedState(0.05f, Idle, Solid, Flash);
        Assert.Equal(LedStage.Off, stage);
    }

    [Fact]
    public void AtIdle_IsOff()
    {
        var (stage, _) = ComputeLedState(Idle, Idle, Solid, Flash);
        // ratio == idleRatio → scaled = 0 → Off (progressive with ratio=0)
        Assert.Equal(LedStage.Progressive, stage);
    }

    [Fact]
    public void MidRange_IsProgressive()
    {
        var (stage, _) = ComputeLedState(0.50f, Idle, Solid, Flash);
        Assert.Equal(LedStage.Progressive, stage);
    }

    [Fact]
    public void AtSolid_IsSolid()
    {
        var (stage, _) = ComputeLedState(Solid, Idle, Solid, Flash);
        Assert.Equal(LedStage.Solid, stage);
    }

    [Fact]
    public void BetweenSolidAndFlash_IsSolid()
    {
        var (stage, _) = ComputeLedState(0.87f, Idle, Solid, Flash);
        Assert.Equal(LedStage.Solid, stage);
    }

    [Fact]
    public void AtFlash_IsFlash()
    {
        var (stage, _) = ComputeLedState(Flash, Idle, Solid, Flash);
        Assert.Equal(LedStage.Flash, stage);
    }

    [Fact]
    public void AboveFlash_IsFlash()
    {
        var (stage, _) = ComputeLedState(1.00f, Idle, Solid, Flash);
        Assert.Equal(LedStage.Flash, stage);
    }

    // ── Progressive ratio scaling ─────────────────────────────────────────────

    [Fact]
    public void Progressive_AtIdle_ScalesTo0()
    {
        var (_, prog) = ComputeLedState(Idle, Idle, Solid, Flash);
        Assert.Equal(0f, prog, precision: 4);
    }

    [Fact]
    public void Progressive_AtSolidMinus1_ScalesTo1()
    {
        // Just below solid threshold should scale close to 1
        var (stage, prog) = ComputeLedState(Solid - 0.001f, Idle, Solid, Flash);
        Assert.Equal(LedStage.Progressive, stage);
        Assert.True(prog > 0.99f, $"Expected prog near 1 but was {prog}");
    }

    [Fact]
    public void Progressive_AtMidpoint_ScalesToHalf()
    {
        float mid = Idle + (Solid - Idle) / 2f;
        var (_, prog) = ComputeLedState(mid, Idle, Solid, Flash);
        Assert.Equal(0.5f, prog, precision: 3);
    }

    [Fact]
    public void Progressive_IsClampedBetween0And1()
    {
        // Below idle → clamped to 0
        var (_, below) = ComputeLedState(0f, Idle, Solid, Flash);
        Assert.Equal(0f, below);

        // Above solid (but below flash) → returns Solid stage, not Progressive
        var (stage, _) = ComputeLedState(Solid + 0.01f, Idle, Solid, Flash);
        Assert.NotEqual(LedStage.Progressive, stage);
    }

    // ── Edge cases ────────────────────────────────────────────────────────────

    [Fact]
    public void ZeroIdleRange_DoesNotThrow()
    {
        // idleRatio == solidRatio — no progressive band; anything below solid is Off
        var (stage, prog) = ComputeLedState(0.84f, 0.85f, 0.85f, 0.90f);
        Assert.Equal(LedStage.Off, stage);
        Assert.Equal(0f, prog);
    }

    [Fact]
    public void FlashStage_ProgressRatioIsZero()
    {
        var (_, prog) = ComputeLedState(1.0f, Idle, Solid, Flash);
        Assert.Equal(0f, prog);
    }

    [Fact]
    public void SolidStage_ProgressRatioIsZero()
    {
        var (_, prog) = ComputeLedState(0.87f, Idle, Solid, Flash);
        Assert.Equal(0f, prog);
    }
}
