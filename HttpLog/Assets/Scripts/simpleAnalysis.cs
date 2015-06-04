using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class simpleAnalysis : MonoBehaviour {
	//total entries
	int logLength;

	//IP address analysis
	List<string> IP = new List<string>();
	List<int> IPCount = new List<int>();

	//Date analysis
	List<string> date = new List<string>();
	List<int> dateCount = new List<int>();

	//request type analysis
	List<string> req = new List<string>();
	List<int> reqCount = new List<int>();

	//website analysis
	List<string> web = new List<string>();
	List<int> webCount = new List<int>();

	//status code analysis
	List<string> status = new List<string>();
	List<int> statusCount = new List<int>();

	//data bytes analysis
	List<int> dataByte = new List<int>();
	float avg;

	//OS analysis
	List<string> OS = new List<string>();
	List<int> OSCount = new List<int>();

	//Log entries
	public Text textLogEntries;
	//IP
	public Text textIPAddress;
	public Text textIPAddressHighest;
	//date
	public Text textDate;
	//req
	public Text textReq;
	//web
	public Text textWeb;
	public Text textWebHighest;
	//status
	public Text textStatus;
	public Text textSuccessRate;
	//data
	public Text textAvgData;
	//os
	public Text textOS;






	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	static int flag=0;
	string[] entries;
	public void analyse()
	{		

		if (flag == 0) {
			flag++;

			logLength=0;

			string line;

			string filePath ="weblog.txt";

			StreamReader theReader = new StreamReader (Application.dataPath+"/"+filePath);
			using (theReader) {
				do {
					line = theReader.ReadLine ();
					
					if (line != null) {
						entries = line.Split (' ');
						if (entries.Length > 0) {

							logEntries();
							IPaddress();
							dateAnalysis();
							reqType();
							webAnalysis();
							statusAnalysis();
							bytesExchange();
							average();
							OSAnalysis();
						}
					}
				} while (line != null);    
				theReader.Close ();
			}

			//printing info to UI
			//log entries
			textLogEntries.text="Total log entries analysed: "+logLength;

			//IP address
			string strIP="";
			for(int i=0;i<IP.Count;i++){
				strIP+=string.Format("{0,-30}  {1,30}",IP[i],IPCount[i]);
				strIP+="\n";
			}
			textIPAddress.text=strIP;
			int max=0;
			int maxIndex=0;
			for(int i=0;i<IPCount.Count;i++){
				if(IPCount[i]>max){
					max=IPCount[i];
					maxIndex=i;
				}
			}
			textIPAddressHighest.text="Highest traffic through \n"+IP[maxIndex]+"\t\t\t\t: "+IPCount[maxIndex];

			//date
			string strDate="";
			for(int i=0;i<date.Count;i++){
				strDate+=date[i]+" May 2009 \t\t\t\t: "+dateCount[i]+"\n";
			}
			textDate.text=strDate;


			//req
			string strReq="";
			for(int i=0;i<req.Count;i++){
				strReq+=req[i]+"\t\t\t\t\t\t: "+reqCount[i]+"\n";
			}
			textReq.text=strReq;

			//web
			string strWeb="";
			for(int i=0;i<web.Count;i++){
				strWeb+=string.Format("{0,-30}  {1,30}","www."+web[i]+".com",webCount[i]);
				strWeb+="\n";
			}
			textWeb.text=strWeb;
			max=0;
			maxIndex=0;
			for(int i=0;i<webCount.Count;i++){
				if(webCount[i]>max){
					max=webCount[i];
					maxIndex=i;
				}
			}
			textWebHighest.text="Highest visits to\nwww."+web[maxIndex]+".com\t\t\t\t: "+webCount[maxIndex];

			//status
			string strStatus="";
			for(int i=0;i<status.Count;i++){
				strStatus+=status[i]+"\t\t\t\t\t: "+statusCount[i]+"\n";
			}
			textStatus.text=strStatus;
			float statusSum=0;
			float successRate=0;
			for(int i=0;i<statusCount.Count;i++){
				statusSum+=statusCount[i];
			}
			successRate=(statusCount[1]/statusSum)*100;
			textSuccessRate.text="Success rate(%): \t\t\t\t\t"+successRate;

			//data
			textAvgData.text="Average Data exchanged(in bytes): \t\t\t"+avg;

			//OS
			string strOS="";
			for(int i=0;i<OS.Count;i++){
				strOS+=OS[i]+"\t\t\t\t\t: "+OSCount[i]+"\n";
			}
			textOS.text=strOS;

		}
	
	}

	void logEntries(){
		logLength++;
	}

	void IPaddress(){
		if (IP.Count == 0) {
			IP.Add (entries [0]);
			IPCount.Add(1);
		}
		else {
			if(IP.Contains(entries[0])){
				IPCount[IP.FindIndex(x => x==entries[0])]++;
			}
			else{
				IP.Add(entries[0]);
				IPCount.Add(1);
			}
		}
	}

	void dateAnalysis(){

		string[] holdDate=entries[3].Split('/');

		if (date.Count == 0) {
			date.Add (holdDate[0]);
			dateCount.Add(1);
		}
		else {
			if(date.Contains(holdDate[0])){
				dateCount[date.FindIndex(x => x==holdDate[0])]++;
			}
			else{
				date.Add(holdDate[0]);
				dateCount.Add(1);
			}
		}
	}

	void reqType(){
		if (req.Count == 0) {
			req.Add (entries [5]);
			reqCount.Add(1);
		}
		else {
			if(req.Contains(entries[5])){
				reqCount[req.FindIndex(x => x==entries[5])]++;
			}
			else{
				req.Add(entries[5]);
				reqCount.Add(1);
			}
		}
	}

	
	void webAnalysis(){
		
		string[] holdWeb=entries[6].Split('/');
		string[] webName=holdWeb[0].Split('.');
		int temp=0;
		if (webName [0] == "www") {
			temp=1;
		}
		if (web.Count == 0) {
			web.Add (webName[temp]);
			webCount.Add(1);
		}
		else {
			if(web.Contains(webName[temp])){
				webCount[web.FindIndex(x => x==webName[temp])]++;
			}
			else{
				web.Add(webName[temp]);
				webCount.Add(1);
			}
		}
	}

	void statusAnalysis(){
		int code;
		int.TryParse (entries [8], out code);
		if (statusCount.Count == 0) {

			status.Add("Informational:");
			status.Add("Success:");
			status.Add("Redirection:");
			status.Add("Client error:");
			status.Add("Server error:");
			for (int i=0; i<5; i++) {
				statusCount.Add (1);
			}
		}
		if (code < 200) {
			statusCount [0]++;
		} 
		else if (code < 300) {
			statusCount[1]++;
		}
		else if (code < 400) {
			statusCount[2]++;
		} 
		else if (code < 500) {
			statusCount[3]++;
		}
		else {
			statusCount[4]++;
		}
	}

	void bytesExchange(){
		int size;
		int.TryParse (entries [9], out size);
		dataByte.Add (size);
	}

	void average(){
		int sum=0;
		for (int i=0; i<dataByte.Count; i++) {
			sum+=dataByte[i];
		}
		avg = sum / dataByte.Count;
	}

	void OSAnalysis(){
		if (OS.Count == 0) {
			OS.Add ("Mac OS:");
			OS.Add ("Windows OS:");
			OS.Add ("Linux OS:");
			OS.Add ("Bots:");

			OSCount.Add (1);
			OSCount.Add (1);
			OSCount.Add (1);
			OSCount.Add (1);
		}
		foreach (string x in entries) {
			if (x.Equals ("Mac")) {
				OSCount [0]++;
			}
			else if (x.Equals ("Windows")) {
				OSCount [1]++;
			} 
			else if (x.Equals ("Linux")) {
				OSCount [2]++;
			}
			else if(x.Contains("bot/")){
				OSCount [3]++;
			}
		}
	}
}



