﻿using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Commands
{
    public class Main : Script
    {
        [Command("estacionarveh")]
        public async Task CMD_estacionarveh(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (player.IsInVehicle)
            {
                if (user.companyProperty != null)
                {
                    Vehicle vehicle = player.Vehicle;

                    if (vehicle.HasData("VEHICLE_BUSINESS_DATA"))
                    {
                        Data.Entities.VehicleCompany veh = vehicle.GetData("VEHICLE_COMPANY_DATA");
                        if(veh.company == user.companyProperty)
                        {
                            veh.spawn = player.Position;
                            veh.spawnRot = player.Heading;

                            await World.Companies.DbFunctions.UpdateCompanyVehicleSpawn(veh.id, player.Position.X, player.Position.Y, player.Position.Z, player.Heading);
                            Utilities.Notifications.SendNotificationOK(player, "Has actualizado las coordenadas de spawn de tu vehículo");
                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo de tu empresa");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo de empresa");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No eres dueño de una empresa");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo");
        }
    }
}
