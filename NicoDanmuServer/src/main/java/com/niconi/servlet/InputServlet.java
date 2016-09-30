package com.niconi.servlet;

import com.niconi.DAO.GlobalValue;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

@WebServlet(name="InputServlet", urlPatterns={"/launch"})
public class InputServlet extends HttpServlet {
	public InputServlet() {
		super();
	}

	@Override
	public void destroy() {
		super.destroy(); 
	}

	@Override
	public void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		this.doPost(request, response);
	}

	@Override
	public void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		response.setCharacterEncoding("UTF-8");
        response.setContentType("text/html;charset=utf-8");

		GlobalValue.queue.offer(request.getParameter("danmu"));
        response.sendRedirect("index.jsp");
	}

	@Override
	public void init() throws ServletException {
		
	}
}
