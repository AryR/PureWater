﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
	public class GetMeasurerResponse : GenericResponse
	{
		public List<Measurer> Measurers;
	}
}