using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

public partial class DownloadImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ImagePath = "DownloadImages/big.png";
        Response.ContentType = "image/jpeg";
        Response.AddHeader("Content-Disposition", "attachment;filename=big.png");
        Response.TransmitFile(Server.MapPath(ImagePath));
        Response.End();
    }
    protected void btnDownloadImageJPEG_Click(object sender, EventArgs e)
    {
        //string allowedExtensions = "mp4,pdf,m4v,gif,jpg,jpeg,png,swf,css,htm,.html";
        //// edit this list to allow file types - do not allow sensitive file types like .cs or .config

        //string fileName = "";
        //string filePath = "";
        //fileName = "images.jpeg";
        //filePath = "DownloadImages/images.jpeg";
        //if (fileName != "" && fileName.IndexOf(".") > 0)
        //{
        //    //bool extensionAllowed = false;
        //    bool extensionAllowed = true;
        //    // get file extension
        //    string fileExtension = fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));

        //    //// check that we are allowed to download this file extension
        //    //string[] extensions = allowedExtensions.Split(',');
        //    //for (int a = 0; a < extensions.Length; a++)
        //    //{
        //    //    if (extensions[a] == fileExtension)
        //    //    {
        //    //        extensionAllowed = true;
        //    //        break;
        //    //    }
        //    //}

        //    if (extensionAllowed)
        //    {
        //        // check to see that the file exists 
        //        if (File.Exists(Server.MapPath(filePath)))
        //        {

        //            // for iphones and ipads, this script can cause problems - especially when trying to view videos, so we will redirect to file if on iphone/ipad
        //            if (Request.UserAgent.ToLower().Contains("iphone") || Request.UserAgent.ToLower().Contains("ipad")) { Response.Redirect(filePath); }


        //            Response.Clear();
        //            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        //            Response.WriteFile(Server.MapPath(filePath));
        //            Response.End();
        //        }
        //        else
        //        {
        //            //litMessage.Text = "File could not be found";
        //        }
        //    }
        //    else
        //    {
        //        //litMessage.Text = "File extension is not allowed";
        //    }
        //}
        //else
        //{
        //    //litMessage.Text = "Error - no file to download";
        //}



        string ImagePath = "DownloadImages/images.jpeg";
        Response.ContentType = "image/jpeg";
        Response.AddHeader("Content-Disposition", "attachment;filename=images.jpeg");
        Response.TransmitFile(Server.MapPath(ImagePath));
        Response.End();


        //string ImagePath = "DownloadImages/images.jpeg";
        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment;filename=images.jpeg");

        //Response.ContentType = "image/jpeg";
        //Response.WriteFile(Server.MapPath(ImagePath));

        //Response.End();

        //try
        //{
        //    // string filename = this.Page.Request.QueryString["FileName"].ToString();
        //    HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create( ConfigurationManager.AppSettings["pageURL"].ToString()+"DownloadImages/images.jpeg");
        //    HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        //    int buffersize = 1;

        //    Response.Clear();
        //    Response.ClearHeaders();
        //    Response.ClearContent();
        //    Response.AppendHeader("Content-Disposition:", "attachment; filename=images.jpeg");
        //    Response.AppendHeader("Content-Length", objResponse.ContentLength.ToString());
        //    Response.ContentType = "application/download";

        //    byte[] byteBuffer = new byte[buffersize + 1];
        //    MemoryStream memStrm = new MemoryStream(byteBuffer, true);
        //    Stream strm = objRequest.GetResponse().GetResponseStream();
        //    byte[] bytes = new byte[buffersize + 1];
        //    while (strm.Read(byteBuffer, 0, byteBuffer.Length) > 0)
        //    {
        //        Response.BinaryWrite(memStrm.ToArray());
        //        Response.Flush();
        //    }
        //    Response.Close();
        //    Response.End();
        //    memStrm.Close();
        //    memStrm.Dispose();
        //    strm.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    ex.Message.ToString();
        //}
    }
    protected void btnDownloadImageJPG_Click(object sender, EventArgs e)
    {
        string ImagePath = "DownloadImages/Blue_hills.jpg";
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=Blue_hills.jpg");
        Response.TransmitFile(Server.MapPath(ImagePath));
        Response.End();
    }
    protected void btnDownloadImagePNG_Click(object sender, EventArgs e)
    {
        string ImagePath = "DownloadImages/big.png";
        Response.ContentType = "image/png";
        Response.AddHeader("Content-Disposition", "attachment;filename=big.png");
        Response.TransmitFile(Server.MapPath(ImagePath));
        Response.End();
    }
    protected void btnDownloadImageGIF_Click(object sender, EventArgs e)
    {
        string ImagePath = "DownloadImages/arrow.gif";
        Response.ContentType = "image/gif";
        Response.AddHeader("Content-Disposition", "attachment;filename=arrow.gif");
        Response.TransmitFile(Server.MapPath(ImagePath));
        Response.End();
    }
    protected void btnDownloadImageBMP_Click(object sender, EventArgs e)
    {
        string ImagePath = "DownloadImages/ManageProfile.bmp";
        Response.ContentType = "image/bmp";
        Response.AddHeader("Content-Disposition", "attachment;filename=ManageProfile.bmp");
        Response.TransmitFile(Server.MapPath(ImagePath));
        Response.End();
    }
}