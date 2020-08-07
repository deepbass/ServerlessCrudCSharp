param(
     [string]
     $Name = "x"
)
$Regions = "eastasia","southeastasia","centralus","eastus", "westus", "northcentralus", "southcentralus",  "northeurope","westeurope", "japaneast", "brazilsouth", "australiasoutheast", "canadacentral", "uksouth", "southafricanorth"

$Regions | ForEach-Object -Parallel {
    func azure functionapp publish "$($using:Name)$($_)"
} -ThrottleLimit 100