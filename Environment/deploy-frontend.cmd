call az storage blob upload-batch -s %1 -d $web --account-name %2
call az storage blob service-properties update --account-name %2 --static-website --index-document index.html --404-document index.html
call az cdn endpoint purge --content-paths /* --resource-group %3 --profile-name %2 --name %2
call az cdn endpoint rule add -g %3 -n %2 --profile-name %2 --order 1 --rule-name "redirect" --match-variable RequestScheme --operator Equal --match-values HTTP --action-name "UrlRedirect" --redirect-protocol Https --redirect-type Found