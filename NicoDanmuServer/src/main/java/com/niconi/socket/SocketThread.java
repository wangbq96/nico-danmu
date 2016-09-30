package com.niconi.socket;

import com.niconi.DAO.GlobalValue;

import java.io.BufferedWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.net.Socket;

public class SocketThread extends Thread
{
    private Socket socket=null;
    private BufferedWriter bw=null;
	private String danmu = null;

    public SocketThread(Socket s) {
    	this.socket=s;
    }
    
    @Override
    public void run() {
    	try {
			bw=new BufferedWriter(
				new OutputStreamWriter(
				 socket.getOutputStream()));
		} catch (IOException e1) {
			e1.printStackTrace();
		}
		System.out.println("SocketThread start");
        while(!socket.isClosed()) {

			danmu = GlobalValue.queue.poll();

        	if(danmu!=null) {
				try {
					bw.write(danmu+"\n");
					bw.flush();
					System.out.println("danmu has launched");
				} catch (IOException e) {
					e.printStackTrace();
					break;
				}
        	}
        }
        try {
			bw.close();
			socket.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
        bw=null;
        socket=null;
    }
}
