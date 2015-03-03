using System;
using System.Collections;

namespace iHRPCore.Com
{
	/// <summary>
	/// Summary description for clsCalendar.
	/// </summary>
	public class clsCalendar
	{
		ArrayList arrDays = new ArrayList();
		public clsCalendar()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Creater: LANHTD
		/// </summary>
		/// <param name="mYear"></param>
		/// <returns></returns>
		public ArrayList GetMonthDays(int mYear)
		{
			ArrayList monthDays = new ArrayList(12);
			monthDays.Add(31);
			monthDays.Add(28);
			monthDays.Add(31);
			monthDays.Add(30);
			monthDays.Add(31);
			monthDays.Add(30);
			monthDays.Add(31);
			monthDays.Add(31);
			monthDays.Add(30);
			monthDays.Add(31);
			monthDays.Add(30);
			monthDays.Add(31);
			if (((mYear % 4 == 0) && (mYear % 100 != 0)) || (mYear % 400 == 0))
				monthDays[1] = 29;
			return monthDays;

		}

		/// <summary>
		/// Creater: LANHTD
		/// Discription: Ham tra ra chuoi format theo ngay/thang
		/// </summary>
		/// <returns>Chuoi format theo ngay/thang</returns>
		public string ToDM(int day,int month)
		{
			return day.ToString() + "/" + month.ToString();
		}

		/// <summary>
		/// Creater: LANHLD
		/// <return>thiet lap mang cac ngay di tu 18 thang nay den 17 thang sau</return>
		/// </summary>
		/// <param name="beg">Ngay bat dau(theo he thong thuong la 18 thang nay)</param>
		/// <param name="end">Ngay ket thuc (theo he thong thuong la 17 thang sau)</param>
		public ArrayList SetTime(string beg, string end)
		{
			ArrayList DayMonth = new ArrayList();
			string[] begday = beg.Split(new char[]{'/'});
			string[] endday = end.Split(new char[]{'/'});
			
			int int_begday = int.Parse(begday[0]);
			int int_begmonth = int.Parse(begday[1]);
			int int_endday = int.Parse(endday[0]);
			int int_endmonth = int.Parse(endday[1]);
			// TINH TONG SO NGAY
			int kc1 = Convert.ToInt16(arrDays[int_begmonth - 1]) - int_begday;
			int kc = 0;
			if(int_begmonth==int_endmonth)
			{
				kc = int_endday - int_begday;
			}
			else
			{
				int kc2 = int_endday;
				kc = kc1 + kc2;
			}
			for(int i=int_begmonth;i<int_endmonth;i++)
			{
				kc += Convert.ToInt16(arrDays[i]);
			}
			// 
			int pmonth = int_begmonth;
			int pday = int_begday - 1 ;
			for(int j=1;j<=kc+1;j++)
			{	
				if(pday>=Convert.ToInt16(arrDays[pmonth - 1]))
				{
					pmonth++;
					pday = 1;
				}
				else
					pday++;
				DayMonth.Add(ToDM(pday,pmonth));
			}
			return DayMonth;
		}

	}
}
