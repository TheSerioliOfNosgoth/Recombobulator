using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MultiSpline : SR1_Structure
	{
		SR1_Pointer<Spline> positional = new SR1_Pointer<Spline>();
		SR1_Pointer<RSpline> rotational = new SR1_Pointer<RSpline>();
		SR1_Pointer<Spline> scaling = new SR1_Pointer<Spline>();
		SR1_Pointer<Spline> color = new SR1_Pointer<Spline>();
		SplineDef curPositional = new SplineDef();
		SplineDef curRotational = new SplineDef();
		SplineDef curScaling = new SplineDef();
		SplineDef curColor = new SplineDef();

		//Matrix curRotMatrix = new Matrix(); // Matrix isnt used/exported?
		MiniMatrix curRotMatrix = new MiniMatrix();
		Spline positionalSpline = new Spline();
		RSpline rotationalSpline = new RSpline();
		Spline scalingSpline = new Spline();
		Spline colorSpline = new Spline();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			positional.Read(reader, this, "positional");
			rotational.Read(reader, this, "rotational");
			scaling.Read(reader, this, "scaling");
			color.Read(reader, this, "color");
			curPositional.Read(reader, this, "curPositional");
			curRotational.Read(reader, this, "curRotational");
			curScaling.Read(reader, this, "curScaling");
			curColor.Read(reader, this, "curColor");

			if (rotational.Offset != 0) curRotMatrix.Read(reader, this, "curRotMatrix");
			if (positional.Offset != 0) positionalSpline.Read(reader, this, "positionalSpline");
			if (rotational.Offset != 0) rotationalSpline.SetPadding(4).Read(reader, this, "rotationalSpline");
			if (scaling.Offset != 0) scalingSpline.Read(reader, this, "scalingSpline");
			if (color.Offset != 0) colorSpline.Read(reader, this, "colorSpline");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			positional.Write(writer);
			rotational.Write(writer);
			scaling.Write(writer);
			color.Write(writer);
			curPositional.Write(writer);
			curRotational.Write(writer);
			curScaling.Write(writer);
			curColor.Write(writer);

			if (rotational.Offset != 0) curRotMatrix.Write(writer);
			if (positional.Offset != 0) positionalSpline.Write(writer);
			if (rotational.Offset != 0) rotationalSpline.Write(writer);
			if (scaling.Offset != 0) scalingSpline.Write(writer);
			if (color.Offset != 0) colorSpline.Write(writer);
		}
	}
}
