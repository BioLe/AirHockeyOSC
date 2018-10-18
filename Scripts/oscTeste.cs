using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class oscTeste : MonoBehaviour {
    
    private Vector2 position;
    private Rigidbody2D rb;

   	public OSC osc;
    public baliza balizaRed, balizaBlue;
    public GameObject playerRed, playerBlue;
    public bola puck;

    private float tempo;

    private bool startSim;

    //Info CSV
    // Time.time | accelerometerRawX, accelerometerRawY | playerPosX, playerPosY, playerVelocity | puckPosX, puckPosY, puckRigidBodyVelocity | scoreBlue, scoreRed
    private List<string[]> rowData = new List<string[]>();

    private static int rowDataTempSize = 12;
    string[] rowDataTemp;

	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();

        osc.SetAddressHandler("/accelerometer/raw/x", OnReceiveX);
        osc.SetAddressHandler("/accelerometer/raw/y", OnReceiveY);

        startSim = false;
        tempo = 0;

        rowDataTemp = new string[rowDataTempSize];
        //Header csv
        rowDataTemp[0] = "Time.time";
        rowDataTemp[1] = "PlayerName";
        rowDataTemp[2] = "accelerometerRawX";
        rowDataTemp[3] = "accelerometerRawY";
        rowDataTemp[4] = "playerPosX";
        rowDataTemp[5] = "playerPosY";
        rowDataTemp[6] = "playerVelocity";
        rowDataTemp[7] = "puckPosX";
        rowDataTemp[8] = "puckPosY";
        rowDataTemp[9] = "puckVelocity";
        rowDataTemp[10] = "scoreBlue";
        rowDataTemp[11] = "scoreRed";
        
        rowData.Add(rowDataTemp);
    }

    void Update(){

        if(balizaBlue.getScored() == true || balizaRed.getScored() == true){
            playerRed.transform.position = new Vector2(0,3);
            playerBlue.transform.position = new Vector2(0,-3);
            balizaRed.setScored(false);
            balizaBlue.setScored(false);
        }

        if(startSim){
            tempo += Time.deltaTime;
            //Adicionar ao array temporario
            rowDataTemp = new string[rowDataTempSize];
            //Info
            rowDataTemp[0] = (tempo).ToString();
            Debug.Log("Tempo: " + rowDataTemp[0]);
            rowDataTemp[1] = this.name;
            //Dados acelerometro
            rowDataTemp[2] = (position.x).ToString();
            rowDataTemp[3] = (position.y).ToString();
            //Dados player
            rowDataTemp[4] = (this.transform.position.x).ToString();
            rowDataTemp[5] = (this.transform.position.y).ToString();
            rowDataTemp[6] = (rb.velocity.magnitude).ToString();
            //Dados puck(bola)
            rowDataTemp[7] = (puck.getPosX()).ToString();
            rowDataTemp[8] = (puck.getPosY()).ToString();
            rowDataTemp[9] = (puck.getVel()).ToString();
            //Score
            rowDataTemp[10] = (balizaRed.getGoalsBlue()).ToString();
            rowDataTemp[11] = (balizaBlue.getGoalsRed()).ToString();

            rowData.Add(rowDataTemp);
        }

    }
	
	void FixedUpdate () {
        
        

        if(startSim)
            rb.AddForce(position * Time.fixedDeltaTime * 200.0f * -1.0f);


        
	}

    private void LateUpdate() {
        if (Input.GetKey("g")) exportCSV();
        if (Input.GetKey("r")) startSim = true;
    }

    void OnReceiveX(OscMessage message) {
        float x = message.GetFloat(0);
        position.x = x;
    }

    void OnReceiveY(OscMessage message) {
        float y = message.GetFloat(0);
        position.y = y;
    }


    private void exportCSV() {
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++) {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        DateTime dt = System.DateTime.Now;
        string datetime = dt.ToString("yyyy-MM-dd_HH-mm-ss");

        String filePath = Application.dataPath + "/" + "data_" + this.name + "_" + datetime + ".csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

}
