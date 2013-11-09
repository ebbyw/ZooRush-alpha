using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioEventHandler : MonoBehaviour
{
	GameObject track2; // Handles Infection and PowerUp Sounds
	GameObject track3; // Handles Building Sounds
	GameObject track4; //Handles Crisis and Situational Sounds
	Dictionary<string,AudioClip> audioClips;
	string[] levelMusicNames = {"INTRO","LEVEL1"};
	bool gameOverPlayed = false;
	
	void Start ()
	{
		audioClips = new Dictionary<string,AudioClip> ();
		audioClips.Add ("CAPTURED", Resources.Load ("Sounds/CAPTURED", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("DOCTOR", Resources.Load ("Sounds/DOCTOR", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("GAMEOVER", Resources.Load ("Sounds/GAMEOVER", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("HARDSICKLOOP", Resources.Load ("Sounds/HARDSICKLOOP", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("INFECTION", Resources.Load ("Sounds/INFECTION", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("JUMP", Resources.Load ("Sounds/JUMP", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("LEVEL1", Resources.Load ("Sounds/LEVEL1", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("PILL", Resources.Load ("Sounds/PILL", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("SOFTSICKLOOP", Resources.Load ("Sounds/SOFTSICKLOOP", typeof(AudioClip)) as AudioClip);
		AudioClip levelMusic;
		audioClips.TryGetValue (levelMusicNames [SceneManager.levelNumber], out levelMusic);
		audio.clip = levelMusic;
		if(OptionsMenu.bgMusicOn){
			audio.Play ();
		}
		
		track2 = new GameObject ("Track 2", typeof(AudioSource));
		track2.audio.playOnAwake = false;
		track2.audio.loop = false;
		track2.transform.parent = transform;
		
		track3 = new GameObject ("Track 3", typeof(AudioSource));
		track3.transform.parent = transform;
		track3.audio.playOnAwake = false;
		track3.audio.loop = false;
		
		track4 = new GameObject ("Track 4", typeof(AudioSource));
		track4.transform.parent = transform;
		track4.audio.playOnAwake = false;
		track4.audio.loop = false;
		
	}
	
	void Update ()
	{
		if (PainIndicator.Crisis && !SceneManager.characterFainted) {
			if (!track4.audio.isPlaying && OptionsMenu.SoundOn) {
				audio.volume = 0.7f;
				AudioClip crisisMusic;
				audioClips.TryGetValue ("HARDSICKLOOP", out crisisMusic);
				track4.audio.clip = crisisMusic;
				track4.audio.loop = true;
				track4.audio.Play ();
			}
		} else {
			audio.volume = 1f;
			track4.audio.Stop ();
		}
		if (SceneManager.characterFainted) {
				if (!gameOverPlayed && OptionsMenu.bgMusicOn) {
					track2.audio.Stop(); // Handles Infection and PowerUp Sounds
					track3.audio.Stop(); // Handles Building Sounds
					track4.audio.Stop(); // Handles Crisis and Situational Sounds
					AudioClip gameOver;
					audioClips.TryGetValue ("GAMEOVER", out gameOver);
					audio.loop = false;
					audio.clip = gameOver;
					audio.Play ();
					gameOverPlayed = true;
				}
		}
	}
	
	public void playInfection ()
	{
		if (!track2.audio.isPlaying && OptionsMenu.SoundOn) {
			AudioClip infectionSound;
			audioClips.TryGetValue ("INFECTION", out infectionSound);
			track2.audio.clip = infectionSound;
			track2.audio.loop = false;
			track2.audio.Play ();
		}
	}
	
	public void playDoctor()
	{
		if (!track3.audio.isPlaying && OptionsMenu.SoundOn) {
			AudioClip doctorSound;
			audioClips.TryGetValue ("DOCTOR", out doctorSound);
			track3.audio.clip = doctorSound;
			track3.audio.loop = false;
			track3.audio.Play ();
		}
	}
	
	public void playPill()
	{
		if (!track2.audio.isPlaying && OptionsMenu.SoundOn) {
			AudioClip pillSound;
			audioClips.TryGetValue ("PILL", out pillSound);
			track2.audio.clip = pillSound;
			track2.audio.loop = false;
			track2.audio.Play ();
		}
	}
}
