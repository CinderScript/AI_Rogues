using ProtoBuf;
using System;
using System.IO;

namespace IronGrimoire.Persistence
{
	class ProtoUtility
	{
		public static Exception SaveToFile<Object>(string filePath, Object serializable)
		{
			Exception ex = null;

			try
			{
				FileInfo fileInfo = new FileInfo( filePath );
				Directory.CreateDirectory( fileInfo.Directory.FullName );

				using (FileStream stream = File.Open( filePath, FileMode.Create ))
				{
					Serializer.Serialize( stream, serializable );
				}
			}
			catch (Exception e)
			{
				ex = e;
			}

			return ex;
		}
		public static Exception LoadFromFile<Object>(string filePath, out Object deserializable)
		{
			Exception ex = null;

			try
			{
				FileInfo fileInfo = new FileInfo( filePath );
				Directory.CreateDirectory( fileInfo.Directory.FullName );

				using (FileStream stream = File.Open( filePath, FileMode.OpenOrCreate ))
				{
					deserializable = Serializer.Deserialize<Object>( stream );
				}
			}
			catch (Exception e)
			{
				deserializable = default( Object );
				ex = e;
			}

			return ex;
		}
	}
}