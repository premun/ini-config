BUILDER=xbuild

.PHONY: config doc clean distclean

config:
	$(BUILDER) /p:TargetFrameworkVersion="v4.0" Config/Config/Config.csproj

doc:
	echo "Not implemented"

distclean:
	$(BUILDER) Config/Config.sln /t:Clean
	rm -rf bin
	rm -rf obj

clean: distclean
	rm -rf doc
