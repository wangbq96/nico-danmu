package com.niconi.socket;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;
import javax.servlet.annotation.WebListener;

@WebListener
public class SocketServiceLoader implements ServletContextListener {
    public void contextDestroyed(ServletContextEvent arg0) {
        System.out.println("ServletContext destroy");
    }

    public void contextInitialized(ServletContextEvent arg0) {
    	System.out.println("ServletContext create");
    	new SocketListener().start();
    }
}
