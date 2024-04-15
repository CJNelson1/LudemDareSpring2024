using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Score {
    Splus = 6,
    S = 5,
    A = 4,
    B = 3,
    C = 2,
    D = 1,
    F = 0
}

public class SigilScorer : MonoBehaviour {
    public float lineWeight = 1.0f;
    public float checkpointWeight = 1.0f;
    public float timeWeight = 1.0f;

    // TODO Do we need an average of all their completed sigil scores?
        // My idea is to keep their best score per sigil/demon in the demondex
    List<Score> scores = new List<Score>();

    public SigilScorer() {

    }

    // Creates a new score for this sigil drawing, adds it to the running total (list of scores F to Splus), and returns it
    public Score AddScore(Sigil sigil, List<LineRenderer> goodLines, List<LineRenderer> badLines, float time) {

        var goodPoints = goodLines.Sum(line => line.positionCount);
        var badPoints = badLines.Sum(line => line.positionCount);
        var ratioPoints = ((float)goodPoints) / ((float)(goodPoints + badPoints));

        var hitCheckpoints = sigil.checkpoints.Sum(checkpoint => checkpoint.completed ? 1 : 0);
        var ratioCheckpoints = (float)hitCheckpoints / (float)sigil.checkpoints.Count;

        var timeRatio = (sigil.goldTime - time) / sigil.goldTime;

        var overallRatio = ((ratioPoints * lineWeight) + (ratioCheckpoints * checkpointWeight) + (timeRatio * timeWeight)) / (lineWeight + checkpointWeight + timeWeight);

        var newScore = this.RatioScore(overallRatio);

        this.scores.Add(newScore);

        return newScore;
    }

    // Basic average currently
    public Score TotalScore() {
        return (Score) (this.scores.Sum(score => (int) score) / this.scores.Count);
    }

    // Expects a float from 0 to 1ish
    public Score RatioScore(float ratio) {
        if (ratio >= 0.97) {
            return Score.Splus;
        } else if (ratio >= 0.95) {
            return Score.S;
        } else if (ratio >= 0.9) {
            return Score.A;
        } else if (ratio >= 0.8) {
            return Score.B;
        } else if (ratio >= 0.7) {
            return Score.C;
        } else if (ratio >= 0.6) {
            return Score.D;
        }

        return Score.F;
    }
}
