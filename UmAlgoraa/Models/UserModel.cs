using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace UmAlgoraa.Models
{
	public class User
	{
		//to make sure that the id will be unique , whic will be aloowed if i relyed only on foregin key
		[Key]
        public String ApplicationUserId { get; set; }

	    [ForeignKey("ApplicationUserId")]
		//virtual make the property lazy loading so that the property wont be load until i activate it and use it
	    public virtual ApplicationUser ApplicationUser { get; set; }
		
		public List<Ads> Ads { get; set; } 

        public User()
		{
		}
	}
}

