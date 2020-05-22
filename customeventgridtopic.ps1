
$vehicleinformation = @{
    id = [System.Guid]::NewGuid()
    subject = "number plate information"
    eventType = "FOURWEELER"
    eventTime = [System.DateTime]::Now
    data = @{
        numberplate = "TS 07 2000"
        colour = "white"
        make = "BMW"
    }
}

$body = "[" + (ConvertTo-Json $vehicleinformation)   +  "]"

Login-AzAccount

Set-AzContext -Subscription asdadasdasd

New-AzResourceGroup -Name "customtopic" -Location "West Europe" -Verbose

New-AzEventGridTopic -ResourceGroupName customtopic -Name vehicletopic -Location 'West Europe' -Verbose

$endpoint  = (Get-AzEventGridTopic -ResourceGroupName customtopic -Name vehicletopic).Endpoint
$keys = (Get-AzEventGridTopicKey -ResourceGroupName customtopic -Name vehicletopic).Key1

New-AzEventGridSubscription -TopicName vehicletopic -EventSubscriptionName "fourvehicleevents" -Endpoint "https://azureforarchitectsthirdedition.azurewebsites.net/runtime/webhooks/EventGrid?functionName=EventGridTrigger2&code=9EtRtqlePA3O0lI5QRkfrwK0aZbDC5WzZUYiMGV7IrhVY1q8uYhSFg==" -ResourceGroupName customtopic -EndpointType webhook -Verbose

Invoke-WebRequest -Uri $endpoint -Headers @{ "aeg-sas-key"=$keys; "contentType"="application/json" } -Body $body -Method Post -Verbose
