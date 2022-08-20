using System.Collections.Generic;
using System.Web.Http;
using AnexWebAPI.Models;
using System.Data;
using AnexWebAPI.Utils;

namespace AnexWebAPI.Controllers
{
    public class ReservationsController : ApiController
    {

        [HttpGet]
        public IEnumerable<ReservationModel> Reservations()
        {
            string W_SQLText = @"SELECT 
                                 ROW_NUMBER() OVER(ORDER BY r.CreateDate) Id, h.Id AS HotelId, h.Name AS HotelName, r.CreateDate AS Date, SUM(Pax) AS TotalPax, '' AS ReservationNumber 
                                 FROM Reservations r
                                 LEFT OUTER JOIN Hotels h ON (h.Id=r.Hotel)
                                 GROUP BY h.Id, h.Name, r.CreateDate
                                 ORDER BY Date ASC";
            // Rezervasyon bilgileri alınıyor
            DataTable dtReservations = AppUtils.GetDataTable(W_SQLText);
            List<ReservationModel> listReservations = AppUtils.MyConvertToListObject<ReservationModel>(dtReservations);

            // Rezervasyon bilgilerine bilet numaraları ekleniyor
            for (int i = 0; i < listReservations.Count; i++)
            {
                W_SQLText = @"SELECT ReservationNo FROM Reservations WHERE Hotel=@P1 AND CreateDate=@P2";
                Dictionary<string, object> W_Params = new Dictionary<string, object>();
                W_Params.Add("@P1", listReservations[i].HotelId);
                W_Params.Add("@P2", listReservations[i].Date);

                // Rezervasyon bilgileri alındı
                DataTable dtReservationNumbers = AppUtils.GetDataTable(W_SQLText, W_Params);
                string W_SplitChar = "";
                for (int a = 0; a < dtReservationNumbers.Rows.Count; a++)
                {
                    listReservations[i].ReservationNumber += W_SplitChar + dtReservationNumbers.Rows[a][0].ToString();
                    W_SplitChar = ";";
                }
            }
            return listReservations; 
        }




    }
}
