using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoVideoBurstApi.Entities;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace DemoVideoBurstApi.Entities
{
    public class ServerResponse
    {
        public enum ResponseCodes
        {
            Success = 200,
            Failure = 100,
            DatabaseInsertionError = 300,
            Internal_Error = 400,
            CannotDelete = 500,
            InvalidAcessToken = 600,
            UserNotAuthorized = 610,
            NoResultFound = 700,
            InvalidParams = 800,
            InvalidOrExpiredCode = 810,
            UserDoesntExistForCompany = 900,
            CompanyidNotProvided = 1000,
            JobUpdationNotAllowed = 1100,
            AlreadyExists = 310,
            AlreadyRegistered = 320,
            NotAllowedToAcceptInvitation = 1200
        }

        public static string GetResponse(ResponseCodes responseCode)
        {
            // Get response message
            string responseMessage = string.Empty;
            if (responseCode == ResponseCodes.Success)
                responseMessage = "Success";
            if (responseCode == ResponseCodes.DatabaseInsertionError)
                responseMessage = "Database Insertion Failed";
            if (responseCode == ResponseCodes.CannotDelete)
                responseMessage = "Data Dependency on Table";
            if (responseCode == ResponseCodes.InvalidAcessToken)
                responseMessage = "Access Token Not Valid";
            if (responseCode == ResponseCodes.NoResultFound)
                responseMessage = "No results found for the provided parameters.";
            if (responseCode == ResponseCodes.InvalidParams)
                responseMessage = "Invalid Parameters";
            if (responseCode == ResponseCodes.UserDoesntExistForCompany)
                responseMessage = "User Doesn't Exist For Company";
            if (responseCode == ResponseCodes.CompanyidNotProvided)
                responseMessage = "Companyid Not Provided";
            if (responseCode == ResponseCodes.JobUpdationNotAllowed)
                responseMessage = "Job not found or updation is not allowed for this job";
            if (responseCode == ResponseCodes.UserNotAuthorized)
                responseMessage = "User not authorized to perform this action.";
            if (responseCode == ResponseCodes.AlreadyExists)
                responseMessage = "The given data already exists.";

            return responseMessage;
        }
     

    }
}