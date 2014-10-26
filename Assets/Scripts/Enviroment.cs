using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class Enviroment 
{
	[SerializeField]
	Cell[] cells;
		
	[SerializeField]
	[HideInInspector]
	int width;
	
	[SerializeField]
	[HideInInspector]
	int height;
	
	public void Fill(int width, int height)
	{
		this.width = width;
		this.height = height;
		
		cells = new Cell[width*height];
	}
	
	public Cell GetCell(int x,int y)
	{				
		if( x < 0 ) x = width - ( -x % width );
		if( y < 0 ) y = height - ( -y % height );
		if( x >= width ) x = x % width;
		if( y >= height ) y = y % height;
		
		int n = x+y*width;
		return cells[n];
	}
	
	public Cell[] GetNeighbors(int x,int y)
	{
		Cell cell = GetCell(x,y);
		if( cell.neighbors == null ){
			List<Cell> neighbors = new List<Cell>();
			if( x>0 ) neighbors.Add( GetCell(x-1,y) );
			if( x<(width-1) ) neighbors.Add( GetCell(x+1,y) );
			if( y>0 ) neighbors.Add( GetCell(x,y-1) );
			if( y<(height-1) ) neighbors.Add( GetCell(x,y+1) );
            cell.neighbors = neighbors.ToArray();
        }
		
		return cell.neighbors;
            /*return new Cell[]{
			GetCell(x+1,y+0),
			//GetCell(x+1,y+1),
			GetCell(x+0,y+1),
			//GetCell(x-1,y+1),
			GetCell(x-1,y+0),
			GetCell(x+0,y-1),
			//GetCell(x+1,y-1),
		};*/
	}
}
