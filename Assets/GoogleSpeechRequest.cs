using UnityEngine;
using System.Collections;

public class GoogleSpeechRequest
{
//	public Content content = new Content();
//	public Audio audio = new Audio();
//
//	public class Content {
//		public string encoding = "LINEAR16";
//		public int sampleRate = 16000;
//	}
//
//	public class Audio {
//		public string content = "empty content";
//
//		public void setAudio(string content) {
//			this.content = content;
//		}
//	}
//
//	public void setAudio(string content) {
//		audio.setAudio (content);
//	}
	public string config = "{ \"encoding:\" \"LINEAR16\", \"sampleRate\": 16000 }";
	public string audio = "{ \"content\": \"audiocontent\" }";

	public void setAudio(string content) {
		audio = "{ \"content\": \"" + content + " }";
	}
}
