﻿using MYTDotNetCore.Database.Model;
using MYTDotNetCore.Domain.Model;

namespace MYTDotNetCore.Domain;

public class TransferResponseModel
{
    public BaseResponseModel Response { get; set; }
}

public class ResultBlogResponseModel
{
    public TblBlog Blog { get; set; } 
}
