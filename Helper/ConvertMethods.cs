﻿using System.ComponentModel;
using System.Data;

namespace Helper
{
    public static partial class ConvertMethods
    {
        public static bool? ToNBoolean(this Object o)
        {
            bool? b;

            try
            {
                if (o.IsNullOrEmpty())
                    return null;

                b = Convert.ToBoolean(o);
            }
            catch (Exception)
            {
                b = null;
            }

            return b;
        }

        public static bool ToBoolean(this Object o)
        {
            bool? b = o.ToNBoolean();

            if (b.HasValue)
            {
                return b.Value;
            }
            else
            {
                throw new ConvertExtensionException();
            }
        }

        public static Char? ToNChar(this Object o)
        {
            Char? c;

            try
            {
                c = Convert.ToChar(o);
            }
            catch (Exception)
            {
                c = null;
            }

            return c;
        }

        public static Char ToChar(this Object o)
        {
            try
            {
                return Convert.ToChar(o);
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static Int16? ToNInt16(this Object o)
        {
            Int16? i;

            try
            {
                if (o.IsNullOrEmpty())
                    return null;

                i = Convert.ToInt16(o);
            }
            catch (Exception)
            {
                i = null;
            }

            return i;
        }

        public static Int16 ToInt16(this Object o)
        {
            try
            {
                return Convert.ToInt16(o);
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static Int32? ToNInt32(this Object o)
        {
            Int32? i;

            try
            {
                if (o.IsNullOrEmpty())
                    return null;

                i = Convert.ToInt32(o);
            }
            catch (Exception)
            {
                i = null;
            }

            return i;
        }

        public static Int32 ToInt32(this Object o)
        {
            try
            {
                return Convert.ToInt32(o);
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static Int64? ToNInt64(this Object o)
        {
            Int64? i;

            try
            {
                if (o.IsNullOrEmpty())
                    return null;

                i = Convert.ToInt64(o);
            }
            catch (Exception)
            {
                i = null;
            }

            return i;
        }

        public static Int64 ToInt64(this Object o)
        {
            try
            {
                if (o.IsNullOrEmpty())
                {
                    return 0;
                }

                return Convert.ToInt64(o);
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static Decimal? ToNDecimal(this Object o)
        {
            Decimal? d;

            try
            {
                if (o == null)
                {
                    return null;
                }

                d = Convert.ToDecimal(o);
            }
            catch (Exception)
            {
                d = null;
            }

            return d;
        }

        public static Decimal ToDecimal(this Object o)
        {
            try
            {
                return Convert.ToDecimal(o);
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static DateTime? ToNDateTime(this Object o)
        {
            DateTime? d;

            try
            {
                if (o == null)
                {
                    return null;
                }

                d = Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                d = null;
            }

            return d;
        }

        public static DateTime ToDateTime(this Object o)
        {
            try
            {
                return Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                if (o.IsDateTime())
                {
                    try
                    {
                        return DateTime.Parse(o.ToString());
                    }
                    catch (Exception)
                    {
                        throw new ConvertExtensionException();
                    }
                }

                throw new ConvertExtensionException();
            }
        }

        public static Double? ToNDouble(this Object o)
        {
            Double? d;

            try
            {
                d = Convert.ToDouble(o);
            }
            catch (Exception)
            {
                d = null;
            }

            return d;
        }

        public static Double ToDouble(this Object o)
        {
            try
            {
                return Convert.ToDouble(o);
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static TimeSpan? ToNTimeSpan(this Object o)
        {
            TimeSpan? t;

            try
            {
                t = TimeSpan.Parse(o.ToString());
            }
            catch (Exception)
            {
                t = null;
            }

            return t;
        }

        public static TimeSpan ToTimeSpan(this Object o)
        {
            try
            {
                return TimeSpan.Parse(o.ToString());
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static IOrderedQueryable<T> ToOrderedQueryable<T>(this IQueryable<T> query)
        {
            return (IOrderedQueryable<T>)query;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static String ToFormatNumber(this Object o)
        {
            try
            {
                return String.Format("{0:0,0.00}", o);//.Replace(",","x").Replace(".",",").Replace("x",".");                
            }
            catch (Exception)
            {
                throw new ConvertExtensionException();
            }
        }

        public static string PorExtenso(this decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += EscrevaParte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                valor_por_extenso += " DE";
                            else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                valor_por_extenso += " DE";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso;
            }
        }

        private static string EscrevaParte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";

                return montagem;
            }
        }
    }
}
