using ProtoBuf;
using System;
using System.IO;

namespace IronGrimoire.Persistence
{
	class ProtoUtility
	{
		public static void SaveToFile<Object>(string filePath, Object serializable)
		{
			try
			{
				FileInfo fileInfo = new FileInfo( filePath );
				Directory.CreateDirectory( fileInfo.Directory.FullName );

				using (FileStream stream = File.Open( filePath, FileMode.Create ))
				{
					Serializer.Serialize( stream, serializable );
				}
			}
			catch
			{
				throw;
			}
		}
		public static Object LoadFromFile<Object>(string filePath, out bool fileFound)
		{
			Object deserializable;

			fileFound = false;

			try
			{
				FileInfo fileInfo = new FileInfo( filePath );
				Directory.CreateDirectory( fileInfo.Directory.FullName );

				if (!fileInfo.Exists)
				{
					fileFound = true;
				}

				using (FileStream stream = File.Open( filePath, FileMode.OpenOrCreate ))
				{
					deserializable = Serializer.Deserialize<Object>( stream );
				}
			}
			catch
			{
				throw;
			}

			return deserializable;
		}
	}
}