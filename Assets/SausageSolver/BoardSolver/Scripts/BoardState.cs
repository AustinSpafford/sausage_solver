using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerVerb
{
	MoveNorth = 0,
	MoveSouth = 1,
	MoveEast = 2,
	MoveWest = 3,
}

public class CellState
{
	public int Height = 1;
	public bool IsGrill = false;
	public bool[] CanBeClimbedByVerb = new bool[4]; // TODO: Cleanup hardcoded value, without hammering Enum.GetNames().

	public CellState Clone()
	{
		CellState result = this.MemberwiseClone() as CellState;

		result.CanBeClimbedByVerb = 
			this.CanBeClimbedByVerb.Clone() as bool[];

		return result;
	}
}

public class BoardConnection
{
	BoardState FromBoard;
	PlayerVerb Verb;
	BoardState ToBoard;
}

public class BoardState
{
	public int CellCountX { get { return cells.GetLength(0); } }
	public int CellCountY { get { return cells.GetLength(1); } }

	public BoardState(
		int cellCountX,
		int cellCountY)
	{
		cells = new CellState[cellCountX, cellCountY];

		for (int xIndex = 0; xIndex < cellCountX; ++xIndex)
		{
			for (int yIndex = 0; yIndex < cellCountY; ++yIndex)
			{
				cells[xIndex, yIndex] = new CellState();

				// RANDOMIZE!!!
				var cell = cells[xIndex, yIndex];
				cell.Height = Random.Range(0, 3);
				cell.IsGrill = ((cell.Height > 0) && (Random.Range(0, 4) == 0));
				cell.CanBeClimbedByVerb[(int)PlayerVerb.MoveNorth] = (Random.Range(0, 10) == 0);
			}
		}
	}

	public BoardState Clone()
	{
		// Start with a shallow-copy.
		BoardState result = this.MemberwiseClone() as BoardState;

		// Convert the cells to a deep-copy.
		{
			result.cells = result.cells.Clone() as CellState[,];
		
			for (int xIndex = 0; xIndex < result.cells.GetLength(0); ++xIndex)
			{
				for (int yIndex = 0; yIndex < result.cells.GetLength(1); ++yIndex)
				{
					result.cells[xIndex, yIndex] = 
						result.cells[xIndex, yIndex].Clone();
				}
			}
		}

		// Convert the connections to deep-copies.
		result.incomingConnections = new List<BoardConnection>(result.incomingConnections);
		result.outgoingConnections = new List<BoardConnection>(result.outgoingConnections);
		
		return result;
	}

	public CellState GetCell(
		int xIndex,
		int yIndex)
	{
		return cells[xIndex, yIndex];
	}

	private CellState[,] cells = null;

	private List<BoardConnection> incomingConnections = new List<BoardConnection>();
	private List<BoardConnection> outgoingConnections = new List<BoardConnection>();
}

