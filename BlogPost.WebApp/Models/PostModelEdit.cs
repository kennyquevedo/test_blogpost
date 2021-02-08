using BlogPost.BLogic.Dto;
using BlogPost.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.WebApp.Models
{
    public class PostModelEdit
    {
        [BindProperty]
        [Required(AllowEmptyStrings =false)]
        public string SelectedStatus { get; set; }
        public List<SelectListItem> StatusList { get; set; }

        [BindProperty]
        public Post SelectedPost { get; set; }

        public string Comment { get; set; }

        public PostModelEdit()
        {
            //
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem("Select Status", "", true));
            statusList.Add(new SelectListItem(StatusValues.Approved.GetDescription(), StatusValues.Approved.ToString(), false));
            statusList.Add(new SelectListItem(StatusValues.Rejected.GetDescription(), StatusValues.Rejected.ToString(), false));
            statusList.Add(new SelectListItem(StatusValues.Published.GetDescription(), StatusValues.Published.ToString(), false));
            statusList.Add(new SelectListItem(StatusValues.Review.GetDescription(), StatusValues.Review.ToString(), false));

            //
            StatusList = statusList;

        }
    }
}
