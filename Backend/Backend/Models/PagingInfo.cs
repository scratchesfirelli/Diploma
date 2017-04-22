﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
  public class PagingInfo
  {
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public int TotalPages
    {
      get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
    }
    public int StartPage
    {
      get
      {
        int temp;
        if (TotalPages <= 10)
        {
          temp = 1;
        }
        else
        {
          // more than 10 total pages so calculate start and end pages
          if (CurrentPage <= 6)
          {
            temp = 1;
          }
          else if (CurrentPage + 4 >= TotalPages)
          {
            temp = TotalPages - 9;
          }
          else
          {
            temp = CurrentPage - 5;
          }
        }
        return temp;
      }
    }

    public int EndPage
    {
      get
      {
        int temp;
        if (TotalPages <= 10)
        {
          temp = TotalPages;
        }
        else
        {
          // more than 10 total pages so calculate start and end pages
          if (CurrentPage <= 6)
          {
            temp = 10;
          }
          else if (CurrentPage + 4 >= TotalPages)
          {
            temp = TotalPages;
          }
          else
          {
            temp = CurrentPage + 4;
          }
        }
        return temp;
      }
    }

    public IEnumerable<int> Pages
    {
      get
      {
        return Enumerable.Range(StartPage, EndPage- StartPage + 1);
      }
    }
  }
}
