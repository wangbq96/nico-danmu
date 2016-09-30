package com.niconi.DAO;

import java.util.Queue;
import java.util.concurrent.ConcurrentLinkedQueue;

/**
 * Created by wangboquan on 2016/9/28.
 */
public class GlobalValue {
    public static Queue<String> queue= new ConcurrentLinkedQueue<>();
}
