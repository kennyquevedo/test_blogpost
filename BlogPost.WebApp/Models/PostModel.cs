using BlogPost.BLogic.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPost.Common;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.WebApp.Models
{
    public class PostModel
    {
        [BindProperty]
        public string SelectedStatus { get; set; }
        public List<SelectListItem> StatusList { get; set; }

        [BindProperty]
        public Post SelectedPost { get; set; }
        public List<Post> Posts { get; set; }

        public PostModel()
        {
            //
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem(StatusValues.Approved.GetDescription(), StatusValues.Approved.ToString(), true));
            statusList.Add(new SelectListItem(StatusValues.Rejected.GetDescription(), StatusValues.Rejected.ToString(), false));
            statusList.Add(new SelectListItem(StatusValues.Published.GetDescription(), StatusValues.Published.ToString(), false));
            statusList.Add(new SelectListItem(StatusValues.Review.GetDescription(), StatusValues.Review.ToString(), false));

            //
            StatusList = statusList;

        }
    }
}
