using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationSheetsDWH.Model
{
		public class Param
		{
				private string name;
				public string datatype;
				public string paramContent;

				public void setParamName(string paramName)
				{
						this.name = "@" + paramName;
				}

				public string getParamName()
				{
						return this.name;
				}

		}

}
