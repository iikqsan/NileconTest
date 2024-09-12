using Microsoft.AspNetCore.Mvc;
using NileconTest.Models;
using NileconTest.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace NileconTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContentService _db;

        public HomeController(ContentService contentService)
        {
            _db = contentService;
        }

        public IActionResult Index()
        {
            var allContent = _db.getAllContent();
            return View(allContent);
        }

        public IActionResult ViewContent(int id)
        {
            ContentModel result = new ContentModel();
            var ID = _db.getContentByID(id);
            if (ID == null)
            {
                return NotFound();
            }
            result.contentID = ID.contentID;

            var title = _db.getContentByID(id);
            if (title == null)
            {
                return NotFound();
            }
            result.contentTitle = title.contentTitle;

            var imgage = _db.getContentByID(id);
            if (imgage == null)
            { 
              return NotFound();
            }
            result.contentImage = imgage.contentImage;

            var description = _db.getContentByID(id);
            if (description == null)
            { 
                return NotFound();
            }
            result.contentDescription = description.contentDescription;
            return View(result);
        }

        public IActionResult AddContent(ContentModel contentObj)
        {
            int id = 1;
            ContentModel finalObj = new ContentModel();

            if (ModelState.IsValid)
            {
                contentObj.contentID = id;
                while (id != null)
                {
                    var countID = _db.getContentByID(id);
                    if (countID == null)
                    {
                        finalObj.contentID = id;
                        finalObj.contentTitle = contentObj.contentTitle;
                        finalObj.contentImage = contentObj.contentImage;
                        finalObj.contentDescription = contentObj.contentDescription;
                        _db.addContent(finalObj);
                        return RedirectToAction("Index");
                    }
                    id++;
                }
            }
            return View(contentObj);
        }

        public IActionResult EditContent(int id, ContentModel newObj)
        {
            ContentModel result = new ContentModel();
            var DbResult = _db.getContentByID(id);
            result.contentID = DbResult.contentID;
            result.contentTitle = DbResult.contentTitle;
            result.contentImage = DbResult.contentImage;
            result.contentDescription = DbResult.contentDescription;

            var findObj = _db.getContentByID(id);
            if (findObj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                result.contentID = findObj.contentID;
                result.contentTitle = newObj.contentTitle;
                result.contentImage = newObj.contentImage;
                result.contentDescription = newObj.contentDescription;
                _db.updateContent(id, newObj);
                return RedirectToAction("ViewContent", new { id = id });
            }
            return View(result);
        }

        public IActionResult DeleteContent(int id)
        {
            var findObj = _db.getContentByID(id);
            if (findObj == null )
            {
                return NotFound();
            }

            _db.removeContent(findObj);
            return RedirectToAction("Index");
        }
    }
}